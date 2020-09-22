create_pattern_for_index index = tail $ cycle (concat (take index (repeat 0) : take index (repeat 1) : take index (repeat 0) : take index (repeat (-1)) : []))
create_dropped_pattern_for_index index = drop input_offset (create_pattern_for_index index)

result_for_index input_as_digits index = last $ digits (sum $ zipWith (*) (create_dropped_pattern_for_index index) dropped_input_as_digits)


calculate input counter 
    | counter >= 99 = input
    | otherwise = calculate (result input) (counter + 1)

day_16b = take 8 $ calculate long_input_as_digits 0


result input_as_digits = map (result_for_index input_as_digits) [5977709..(5977709+8)] 

input_value = 79171430 
input_offset = 5977709
dropped_input_as_digits = drop input_offset (digits input_value)

-- get_lcm input pattern_index = lcm (length (digits input)) (4 * pattern_index)
-- take lcm of the input 

-- Get separate digits of an integer
digits :: Integer -> [Integer]
digits n = map (\x -> read [x] :: Integer) (show n)

-- from_digits = foldl add_digit 0
--    where add_digit num d = 10*num + d

-- 650 digits -> 6500000 values
-- 6500000 - 5977709 = 522291 values to calculate


-- [7,9,1,7,1,4,3,0] 
-- combine with pattern at 5977709..(5977709+8)



-- 