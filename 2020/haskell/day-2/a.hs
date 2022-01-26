import Data.List

stoi :: [String] -> [Int]
stoi = map read

-- split each element from the string
split :: Eq a => a -> [a] -> [[a]]
split d [] = []
split d s = x : split d (drop 1 y) where (x,y) = span (/= d) s


separate id str = (lst !! 0, lst !! 1) where lst = split id str

data InputElement = InputElement { 
    min :: Int,
    max :: Int,
    id :: String, 
    password :: String
} deriving (Show)


toInputElement = let result = InputElement {
        min,
        max,
        id,
        password
    } where 
        min = 




main = do
    text <- readFile "input.txt"





    -- split between :
    let splittedOnce = map (\elem -> separate ':' elem) $ lines text
    let splittedTwice = map (\(a, b) -> (separate ' ' a, tail b)) $ splittedOnce
    let splittedThrice = map (\((a, b), c) -> ((separate '-' a, b), c)) $ splittedTwice
    let result = map (\(((x, y), b), c) -> (((stoi x, stoi y), b), c)) $ splittedThrice
    print $ "Part one"
    -- print $ read splittedTwice



