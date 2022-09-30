# SVTRobotDistance

Instructions for running:
1) Load the prodject in visual studio (written in 2022) and run it.
2) An pre-built executable can be found in SVTRobotDistance/ReleasePublic/


Possible Future Features:
1) Mess around with the batterylife information, preferring higher/lower battery life. Especially if there's one with a full charge not getting used very much.
3) Actually make a DAL rather than shoving it in with everything else. Also perhaps look for ways to minimize requests on the robot list (Do i actually need to get the entire list every time?). Being able to use one pull for multiple queries would justify the effort of setting up a nice quickly searchable datastructure rather than looping the whole thing.
4) Better error handling and data sanitization for the robot list and post request.