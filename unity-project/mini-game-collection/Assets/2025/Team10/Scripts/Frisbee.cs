
using UnityEngine;

namespace MiniGameCollection.Games2025.Team10
{
    public class Frisbee : MiniGameBehaviour
    {
        [field: SerializeField] public FrisbeeOwner Owner { get; private set; } // Keeps track of which player threw the frisbee
        [field: SerializeField] public Rigidbody2D Rigidbody2D { get; private set; } // Allows the frisbee to move
        [field: SerializeField] public float SelfDestructTime { get; private set; } = 10f; // Deletes frisbee after certain ammount of seconds (Lag preventer)
        [field: SerializeField] public int PointPenalty { get; set; } = -100; // How many points are subtracted when a player gets hit

        [field: ReadOnlyGUI]
        [field: SerializeField] public ScoreKeeper ScoreKeeper { get; private set; } // Used to change the players score

        // Bounce-back settings
        [field: SerializeField] public bool CanBounceBack { get; private set; } = true; // Whether or not the frisbee can bounce back when it hits the wall
        [field: SerializeField] public float ReturnSpeedMultiplier { get; private set; } = 0.8f; // When it bounces off a wall it slows its speed down just a little bit

        private bool isReturning = false; // If the frisbee is own its way back to the player that threw it

      
        public void Shoot(FrisbeeOwner owner, Vector2 direction, float speed, ScoreKeeper scoreKeeper, MiniGameManager miniGameManager)
        {
            // Save references
            ScoreKeeper = scoreKeeper;
            MiniGameManager = miniGameManager;
            this.Owner = owner;

            // Launch the frisbee in the direction the player was facing
            Rigidbody2D.velocity = direction * speed;

            // Incase something goes wrong delete it after how many seconds (SelfDestructTime)
            Destroy(this.gameObject, SelfDestructTime);
        }

        private PlayerID OwnerToPlayerID(FrisbeeOwner owner)
        {
            // Converts the frisbee owner (used in this script) to the PlayerID script which is used in the score system
            return owner switch
            {
                FrisbeeOwner.Player1 => PlayerID.Player1,
                FrisbeeOwner.Player2 => PlayerID.Player2,
                _ => throw new System.NotImplementedException(),
            };
        }

        private void DestroyFrisbee()
        {
            // Deletes the frisbee
            Destroy(this.gameObject); 
        }

        protected override void OnGameEnd()
        {
            // Makes sure all frisbees are deleted when the game ends
            DestroyFrisbee();
        }

        // This function handles the collision stuff
        private void OnTriggerEnter2D(Collider2D collider2d)
        {
            // If the frisbee hits a player
            var player = collider2d.GetComponent<PlayerController>();
            if (player != null)
            {
                // If the frisbee hits the player that threw it
                if (player.PlayerID == OwnerToPlayerID(Owner))
                {
                    // If it's returning, that means the player caught it
                    if (isReturning)
                    {
                        // Player catches their own frisbee successfully
                        player.OnCatchFrisbee(); // No score change for catching your own frisbee :D 
                        
                        DestroyFrisbee();// Delete the frisbee
                        return;
                    }
                    else
                    {
                        // If they somehow hit themselves before the bounce, subtract points
                     
                        ScoreKeeper.AddScore(player.PlayerID, PointPenalty);
                        DestroyFrisbee();
                        return;
                    }
                }
                else
                {
                    // If the frisbee hits the other player
                    // Subtract points from whoever was hit (even if you hit urself)
                    ScoreKeeper.AddScore(player.PlayerID, PointPenalty);
                    DestroyFrisbee();
                    return;
                }
            }

            // Check if it hit a wall
            bool hitLeftWall = collider2d.CompareTag("LeftWall");
            bool hitRightWall = collider2d.CompareTag("RightWall");

            // If not a wall, ignore this collision
            if (!hitLeftWall && !hitRightWall)
            {
                return; // hit something else, ignore for now
            }

            // If it is a wall then figure out which wall was hit
            HandleWallHit(hitLeftWall);
        }


        // Function for what to do if the frisbee hits a wall
        private void HandleWallHit(bool isLeftWall)
        {
            PlayerID ownerID = OwnerToPlayerID(Owner);
            bool ownerIsLeft = ownerID == PlayerID.Player1; // Player one is on left side

            if (!isReturning)
            {
                // Going forward not yet bounced
                bool hitOppWall =
                    (ownerIsLeft && !isLeftWall) ||   // Player 1 hits right wall
                    (!ownerIsLeft && isLeftWall);     // Player 2 hits left wall

                if (hitOppWall && CanBounceBack)
                {   
                    // Bounce back
                    StartReturn(); 
                }
                else
                {
                    // If it somehow magically hits its own wall on launch then destroy it
                    DestroyFrisbee();
                }
            }
            else
            {
                // Already bounced and now returning
                bool hitOwnerWall =
                    (ownerIsLeft && isLeftWall) ||    // Player 1 returning cause hits left wall
                    (!ownerIsLeft && !isLeftWall);    // Player 2 returning cause hits right wall

                // When it hits its own wall again then destroy it
                if (hitOwnerWall)
                {
                    DestroyFrisbee();
                }
            }
        }

        private void StartReturn()
        {
            isReturning = true;
            // Just reverse direction like a boomerang and slow it down slightly 
            Rigidbody2D.velocity = -Rigidbody2D.velocity * ReturnSpeedMultiplier;
        }

        private void OnValidate()
        {
            // Just makes sure the Rigidbody2D isnt missing
            if (Rigidbody2D == null)
            {
                Rigidbody2D = GetComponent<Rigidbody2D>();
            }   
        }
    }
}