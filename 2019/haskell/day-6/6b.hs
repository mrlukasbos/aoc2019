import Data.List

main = do
    text <- readFile "input.txt"
    print $ result $ map separate $ lines text

separate str = (lst !! 0, lst !! 1) where lst = split ')' str

track_key key objs = track (find_by_val key objs) objs []

result objs = let 
        you = (track_key "YOU" objs)
        san = (track_key "SAN" objs)
        in length (you ++ san) - 2 * (length $ intersect you san) - 2 

track obj objs tr
    | fst obj == "COM" = tr ++ [obj]
    | otherwise = track (find_parent obj objs) objs (tr ++ [obj])

find_parent obj objs = find_by_val (fst obj) objs

find_by_val val objs = (filter((== val) . snd) objs) !! 0

split :: Eq a => a -> [a] -> [[a]]
split d [] = []
split d s = x : split d (drop 1 y) where (x,y) = span (/= d) s
