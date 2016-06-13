# 3DRacer
A simple manouvering game where you have to dodge blocks to make it to the endzone as fast as you can. Made using XNA 4.0.

#This game contains some copyrighted work. All copyrighted work belongs to it's respective owners. This program is used for educational uses only and therefore falls under fair use. (Copyright Law of the United States, Chapter 1: Subject Matter and Scope of Copyright, § 107.1)

### TODO List
- [ ] Add more realistic acceleration (gear shifts etc.)
- [ ] Add handler for game over when colliding with blocks
- [ ] Add ability to load map from text file
- [ ] Create Game over

#### Stretch goals/optional feautures
- [ ] Texture for car hood
- [ ] Speedometer
- [ ] Ray trace graphics
- [ ] Add turns to track (Maybe circular track)


                if (gameTime.TotalGameTime.Seconds <= 5)
                {
                    spriteBatch.DrawString(font, "Press W to drive, get to the end as quickly as possible!", new Vector2(150, 500), Color.Red);
                    spriteBatch.DrawString(font, "Press A and D to turn left and Right, speed turns up with L", new Vector2(125, 550), Color.Red);
                }
