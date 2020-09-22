
main = print output

output = sum $ map check_password input

input = [235741..706948]

check_password pwd = fromEnum (is_ok $ digits pwd) where is_ok ds = has_subs ds && is_increasing ds

-- check for duplicates. not very elegant but it works
has_subs ds = ds !! 0 == ds !! 1 || ds !! 1 == ds !! 2  || ds !! 2 == ds !! 3  || ds !! 3 == ds !! 4  || ds !! 4 == ds !! 5

-- check for increasing digits. not very elegant but it works
is_increasing ds = ds !! 0 <= ds !! 1 && ds !! 1 <= ds !! 2  && ds !! 2 <= ds !! 3  && ds !! 3 <= ds !! 4  && ds !! 4 <= ds !! 5

digits :: Integer -> [Int]
digits n = map (\x -> read [x] :: Int) (show n)