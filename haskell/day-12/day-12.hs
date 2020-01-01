{-# LANGUAGE RecordWildCards #-}

import Data.List


data Moon = Moon {
    identifier :: Int, -- useful for debugging
    pos :: (Int, Int, Int),
    vel :: (Int, Int, Int)
} deriving (Show)


day_12a steps = sum $ map total_energy (do_steps all_moons 0 steps) -- get the sum of total energy of the system


do_steps_with_mem :: [Moon] -> Int -> Int -> [Moon]
do_steps_with_mem moons amount max_amount
    | amount >= max_amount = moons
    | otherwise = do_steps (do_step moons) (amount + 1) max_amount


do_steps :: [Moon] -> Int -> Int -> [Moon]
do_steps moons amount max_amount
    | amount >= max_amount = moons
    | otherwise = do_steps (do_step moons) (amount + 1) max_amount

initial_positions :: [(Int, Int, Int)]
initial_positions = [(-1, 7, 3), 
                    (12, 2, -13), 
                    (14, 18, -8), 
                    (17, 4, -4)]

create_new_moon :: Int -> (Int, Int, Int) -> Moon                 
create_new_moon ident init_pos = Moon {
    identifier = ident,
    pos = init_pos,
    vel = (0,0,0)
}

all_moons :: [Moon]
all_moons = zipWith create_new_moon [0..] initial_positions


do_step :: [Moon] -> [Moon]
do_step moons = apply_velocities (apply_gravity moons)

-- apply gravity for each moon, changing their velocity vectors but not their positions
apply_velocities :: [Moon] -> [Moon]
apply_velocities moons = map apply_velocity moons

apply_velocity :: Moon -> Moon
apply_velocity Moon{..} = Moon {
    identifier = identifier,
    pos = add_tup pos vel,
    vel = vel
}

-- apply the first with all others, apply the second with all others... etc
apply_gravity :: [Moon] -> [Moon]
apply_gravity moons = map (compare_moon_with_moons moons) moons

compare_moon_with_moons :: [Moon] -> Moon -> Moon
compare_moon_with_moons other_moons moon = foldl (compare_moons) moon other_moons

-- compare a moon with another moon and return the new 'moon state'
compare_moons :: Moon -> Moon -> Moon
compare_moons current_moon other_moon = let 
    (pcx, pcy, pcz) = pos current_moon
    (pox, poy, poz) = pos other_moon
    new_vel_diff = ((calc_acc pcx pox), (calc_acc pcy poy), (calc_acc pcz poz))
    new_vel = add_tup (vel current_moon) new_vel_diff

    in Moon {
        identifier = identifier current_moon,
        vel = new_vel,
        pos = pos current_moon
     } 

-- compare two values on an axis and return the result
calc_acc :: Int -> Int -> Int
calc_acc a b 
    | a < b = 1
    | a > b = -1
    | otherwise = 0

-- add one tuple to another
add_tup :: (Int, Int, Int) -> (Int, Int, Int) -> (Int, Int, Int)
add_tup (a0, b0, c0) (a1, b1, c1) = (a0+a1, b0+b1, c0+c1) 


pairs :: [a] -> [(a, a)]
pairs l = [(x,y) | (x:ys) <- tails l, y <- ys]


potential_energy Moon{..} = sum_of_abs_coords pos
kinetic_energy Moon{..} = sum_of_abs_coords vel
total_energy moon = (potential_energy moon) * (kinetic_energy moon)

sum_of_abs_coords :: (Int, Int, Int) -> Int
sum_of_abs_coords (a,b,c) = sum [abs a, abs b, abs c]