
main = print output

output = sum $ map check_password [235741..706948]
check_password pwd = fromEnum (is_ok (digits pwd)) where is_ok ds = has_subs ds && is_increasing ds

-- useful for debugging
-- output = filter check_password [235741..706948]
-- check_password pwd = is_ok (digits pwd) where is_ok ds = has_subs ds && is_increasing ds

-- check for duplicates. not very elegant but it works
has_subs :: [Int] -> Bool
has_subs ds = same_as_previous ds 0 0

-- it is the amount of digits in the row
same_as_previous :: [Int] -> Int -> Int -> Bool
same_as_previous ds prev it
    | ds == [] = it == 2 -- when the list is exhausted we return false except if it == 2
    | it == 0 = same_as_previous (tail ds) (head ds) 1 -- the first element
    | (head ds) == prev && it == 1 = same_as_previous (tail ds) (head ds) 2 -- the second element
    | (head ds) /= prev && it == 2 = True -- if the third iteration is different we are fine
    | (head ds) == prev && it >= 2 = same_as_previous (tail ds) (head ds) (it+1)
    | (head ds) /= prev = same_as_previous (tail ds) (head ds) 1 
    | otherwise = same_as_previous (tail ds) (head ds) (it+1)

-- check for increasing digits. not very elegant but it works
is_increasing ds = ds !! 0 <= ds !! 1 && ds !! 1 <= ds !! 2  && ds !! 2 <= ds !! 3  && ds !! 3 <= ds !! 4  && ds !! 4 <= ds !! 5

digits :: Integer -> [Int]
digits n = map (\x -> read [x] :: Int) (show n)
