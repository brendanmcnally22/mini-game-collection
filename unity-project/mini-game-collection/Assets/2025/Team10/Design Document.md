#### **Design Rationale**
For DevCon #2 we are creating and developing a game for an arcade cabinet that will be playable for people interested in what the Game Design program has to offer. Our game Killer Frisbee is made around timing, prediction and risk, where the main idea is to throw a frisbee to try and hit your opponent, if you hit your opponent you regain your frisbee instantly, if you miss it bounces back and you must try to catch it to avoid a cooldown, if you fail to catch your frisbee you have to wait out the cooldown before you can throw a frisbee again. 

Instead of allowing the players to just spam attacks forever, each throw you make has consequences, which all allow to maintain a flow of the game. This idea highlights skill over luck and makes players think carefully about when to attack, how to position themselves, as well as how to react to their opponents frisbee all at the same time. Each successful throw and catch sequence feels rewarding as it requires situational awareness, quick reflexes, and predication of both the frisbee returns and opponents actions.

The rationale behind our game Killer Frisbee is to explore how these mechanics can make a natural feeling flow state in a short 1v1 arcade game. The game is like a test of how momentum, timing, and balance interact inside a faced-paced environment where every input matters.

#### **Objective**
The main objective for Killer Frisbee is to try and test whether a boomerang style frisbee mechanic that tries to reward timing and accuracy can be enough to make a fun competitive game.

The question guiding us is: 

	"**Can a throw and catch system (Frisbee) make a short arcade duel more engaging and skills based, while being super easy to just jump right into with no prior knowledge?**"

We designed the prototype based around three goals: 

- **Rewarding precision and timing** - Players who aim well and catch their frisbees consistently will be able to maintain a fast paced gameplay and putting lots of pressure on their opponent

- **Spatial awareness** - Missing a throw forces players to move around to line up their returning frisbee while also making sure to avoid the opponents attack as well

- **Punishing recklessness by interrupting a players rhythm** - A missed catch triggers a cooldown that you have to wait for until you are able to throw again, allowing the opponents to act without needing to worry about your attack

The prototypes goal is to gather feedback on how players respond to our core idea of the frisbee, the risk and reward cycle of throwing, vs waiting, and whether the or not the core mechanic feels fair, fun and easy enough to understand.

#### **Core Statement**
Our core statement is to make and test out a two player competitive arcade style prototype build around a throw and catch (Frisbee) mechanic, where a successful frisbee hit rewards with instant rethrows and a missed catch of your frisbee creates cooldown penalties. With all of this encouraging rhythm based gameplay. 

#### **Main Gameplay Overview**
Killer Frisbee is a 1v1 top down arena duel. Each round should last around 45-60 seconds, allowing for easy repeatable playing sessions that all feel exciting intense and fair. 

**Gameplay**: 
- **Starting phase** - Each player begins on their sides inside a rectangular arena. They both start with a frisbee that can be thrown at the opponent. The match begins with a small brief cooldown (3 seconds should work), giving players a moment to try and position themselves. 
- **Throwing Frisbee's** - Players press their buttons on the arcade machine to launch their frisbee toward the opponent. This frisbee moves at a set speed and will always return after reaching its maximum distance. The direction of the frisbee is determined by the players aim when releasing it. 
- **Impact and reward logic** - If the frisbee hits the opponent then it will instantly respawn in your hand available to throw again. If it misses then it bounces back off the wall giving another chance to hit the enemy, if it misses it comes back to you and you must catch it or you will receive a cooldown.4
- **Defense Phase** - While managing your own frisbee's, players must also avoid the opponents. Getting hit contributes to the opponents score. Since both of the frisbee's can be on the screen at once, it makes you focus on your spatial awareness heavily.
- **Scoring and end of game** - Points are rewarded for hitting the opponents. When the game timer ends, the player with the highest score will win.

The gameplay experience is a blend of focus and chaos where every second counts. 

#### **Team Roles** 
**Ciaran (Programmer / Designer)** - Is responsible for implementing the player movement, the throw and catch logic + frisbee bounce back. Will also refine timing for throwing and catching and cooldowns. Also develop and make the design document.
**Brendan (Programmer)** - Focuses on the frisbee catching, and the respawning logic. Will also handle the detection for successful hits, and cooldown triggers making sure that it all works smoothly. Also works closely with Ciaran on debugging and gameplay refinement.
**Michael (UX / UI Designer)** - Designs the visual interface for the match, including the timer, scores and cooldown indicators. Creates clear on screen feedback for catches, misses, and successful hits, making sure that players always understand what state they are in and why.
**Jayce (Level Designer)** - Responsible for designing and refining the arena layout and player boundaries. And experiment with different assets to make visually appealing and fun level layouts.    

#### **Technical Notes**
- Engine: Unity 3D (RP) Version 2022.3.62f2
- Version Control: Git + Git LFS

#### **Scripts**
For scripts, pretty much all the scripts we have were already there as a basis for us to start with. Ciaran first when in and looked at the code, mainly the PlayerController and Bullet script (now called frisbee), and looked and tried to understand how it all works. Then after he started to work on making the code work for our game. 

````csharp

````

````csharp

````