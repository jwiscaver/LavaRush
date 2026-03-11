Purpose:



Tracks systems already implemented.



\## Player Controller System



The player controller is implemented using Rigidbody2D physics.



Movement:

\- Horizontal velocity applied directly to Rigidbody2D.velocity

\- Physics updates occur in FixedUpdate



Jump system:

\- Jump buffering (~0.1s)

\- Coyote time (~0.15s)



Ground detection:

\- Physics2D.OverlapCircle using a GroundCheck transform



Input:

\- Unity Input System

\- GameInput.inputactions is the single authoritative input asset

