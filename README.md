# udacityReinforcement
Reinforcement implementation of udacitys self-driving car simulation:
https://github.com/udacity/self-driving-car-sim
Coding changes are in the Assets/1_SelfDrivingCar/Scripts folder
As well as this, there are some cosmetic changes, including adding a road divider between two separate roads

The development of a reward function:
Penalty for being stuck, driving off the road.
Reward for driving further.
More or less penalty based on how the vehicle is stuck.

Road Detection system:
Checks when any of the tires are off the road, used for resetting as well as data collection for road detection algorithms.

Segmentation system:
Duplicates the RGB world, and copies movements of the car.
Used for data collection \& giving a reward based on location in lane.

The reward functions are transferable from simulation to real-world with transfer learning.
