{-# LANGUAGE RecordWildCards #-}

import Data.List

data Moon = Moon {
    identifier :: Int, -- useful for debugging
    pos :: Int,
    vel :: Int
} deriving (Show, Eq)

day_12b = lcms [(do_steps_with_mem (all_moons initial_positions_x) (all_moons initial_positions_x) 0), (do_steps_with_mem (all_moons initial_positions_y) (all_moons initial_positions_y) 0), (do_steps_with_mem (all_moons initial_positions_z) (all_moons initial_positions_z) 0)]

do_steps_with_mem :: [Moon] -> [Moon] -> Int -> Int
do_steps_with_mem mem ref iteration
    | (new_moon == ref) = (iteration + 1)
    | otherwise =  do_steps_with_mem new_moon ref  (iteration + 1)
    where new_moon = (do_step mem)


do_steps :: [Moon] -> Int -> Int -> [Moon]
do_steps moons amount max_amount
    | amount >= max_amount = moons
    | otherwise = do_steps (do_step moons) (amount + 1) max_amount

initial_positions_x = [-1, 12, 14, 17]
initial_positions_y = [7, 2, 18, 4]
initial_positions_z = [3, -13, -8, -4]

create_new_moon :: Int -> Int -> Moon                 
create_new_moon ident init_pos = Moon {
    identifier = ident,
    pos = init_pos,
    vel = 0
}

all_moons :: [Int] -> [Moon]
all_moons positions = zipWith create_new_moon [0..] positions


do_step :: [Moon] -> [Moon]
do_step moons = apply_velocities (apply_gravity moons)

-- apply gravity for each moon, changing their velocity vectors but not their positions
apply_velocities :: [Moon] -> [Moon]
apply_velocities moons = map apply_velocity moons

apply_velocity :: Moon -> Moon
apply_velocity Moon{..} = Moon {
    identifier = identifier,
    pos = pos + vel,
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
    pc = pos current_moon
    po = pos other_moon
    new_vel_diff = (calc_acc pc po)
    new_vel = (vel current_moon) + new_vel_diff

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


lcms :: (Integral a) => [a] -> a
lcms [] = 1
lcms [x] = x
lcms (x:xs) = lcm x (lcms xs)