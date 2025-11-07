using UnityEngine;

namespace MiniGameCollection.Games2025.Team10
{
    public class PlayerController : MiniGameBehaviour
    {
        // Player setup stuff
        [field: SerializeField] public PlayerID PlayerID { get; private set; }
        [field: SerializeField] public GameObject FrisbeePrefab { get; private set; }
        [field: SerializeField] public Rigidbody2D Rigidbody2D { get; private set; }
        [field: SerializeField] public ScoreKeeper ScoreKeeper { get; private set; }
        [field: SerializeField] public float FrisbeeSpeed { get; private set; } = 8f; // units per second
        [field: SerializeField] public float ShipSpeed { get; private set; } = 10f; // units per second
        [field: SerializeField] public float MinMaxY { get; private set; } = 4f; // constraints along Y axis movement
        [field: SerializeField] public bool CanShootFrisbee { get; private set; } = false;


        // X movement limits
        [field: SerializeField] public float MinX { get; private set; } = -8f; // Left movement limiter for Player 1
        [field: SerializeField] public float MaxX { get; private set; } = 8f; // Right limited for Player 2
        [field: SerializeField] public float MiddleClamp { get; private set; } = 1.5f; // Middle zone where players cant go (no middle camping)

        // Shooting cooldown timer
        [field: SerializeField] public float FireCooldown { get; private set; } = 1f; // Temporary firing cooldown for testing (will be changed according to bounce back of frisbee feature)
        private float fireTimer = 0f; // Tracks time since last frisbee was thrown 

        //
        private Vector2 moveInput = Vector2.zero;

        // Identifys which player own the frisbee
        private FrisbeeOwner Owner => PlayerID switch
        {
            PlayerID.Player1 => FrisbeeOwner.Player1,
            PlayerID.Player2 => FrisbeeOwner.Player2,
            _ => throw new System.Exception(),
        };


        void Update()
        {
            // Get both X and Y inputs for players
            float axisY = ArcadeInput.Players[(int)PlayerID].AxisY;
            float axisX = ArcadeInput.Players[(int)PlayerID].AxisX;

            moveInput = new Vector2(axisX, axisY);

            // Shooting Frisbee logic
            if (!CanShootFrisbee)
            {
                return; // If player isnt allowed to shoot, skip the rest
            }

            // Count down cooldown timer
            fireTimer -= Time.deltaTime;

            // If the cooldown is done and shoot button is pressed -> shoot the frisbee
            if (fireTimer < 0f && ArcadeInput.Players[(int)PlayerID].Action1.Pressed)
            {
                ShootFrisbee();
                fireTimer = FireCooldown; // Reset the cooldown timer
            }

        }

        // Old movement was in Update but it felt really jittery (I moved it to FixedUpdate and it seems less laggy - Ciaran)
        private void FixedUpdate()
        {
            Vector2 movement = moveInput * ShipSpeed * Time.deltaTime;

            // Start from Rigidbody position for the physics
            Vector2 newPosition = Rigidbody2D.position + movement;

            // Clamp the bottom and top of the screen
            newPosition.y = Mathf.Clamp(newPosition.y, -MinMaxY, MinMaxY);

            // Keeps players on their sides of the map
            if (PlayerID == PlayerID.Player1)
            {
                // Red stays on left side of screen
                newPosition.x = Mathf.Clamp(newPosition.x, MinX, -MiddleClamp);
            }
            else if (PlayerID == PlayerID.Player2)
            {
                // Blue stays on right side of screen
                newPosition.x = Mathf.Clamp(newPosition.x, MiddleClamp, MaxX);
            }

            // Actually moves the players Rigidbody2D
            Rigidbody2D.MovePosition(newPosition);
        }

        void ShootFrisbee()
        {
            // Figure out where to spawn the frisbee (in front of the player)
            Vector3 position = transform.position + transform.up;

            // Make a copy of the frisbee prefab
            GameObject prefab = Instantiate(FrisbeePrefab, position, transform.rotation);

            // Gets the frisbee script atattched to the frisbee prefab
            Frisbee frisbee = prefab.GetComponent<Frisbee>();

            // Inform the script who threw it, and how fast to go 
            frisbee.Shoot(Owner, transform.up, FrisbeeSpeed, ScoreKeeper, MiniGameManager);
        }
        public void OnCatchFrisbee()
        {
            // Player successfully caught their returning frisbee
            // You get to shoot that thing again twin
            CanShootFrisbee = true;

            
        }
        /// <summary>
        /// Hey Ciaran
        /// I was able to add the catch mechanic and the players can throw the frisbee again after catching it.
        ///  Catching only works after bounce (isReturning = true)
        //   Players must have PlayerController + correct PlayerID
        //   Wall tags: "LeftWall" and "RightWall"
        /// </summary>
        private void OnValidate()
        {
            // Automatically fills in missing Rigidbody refernece in the Unity Inspector
            if (Rigidbody2D == null)
            {
                Rigidbody2D = GetComponent<Rigidbody2D>();
            }
        }

        protected override void OnGameStart()
        {
            // Allows the player to shoot when the game starts 
            CanShootFrisbee = true;
        }

        protected override void OnGameEnd()
        {
            // Stops the player from shooting when the game ends 
            CanShootFrisbee = false;
        }
    }
}