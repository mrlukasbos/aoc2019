import Data.List.Split
import Data.List
import Data.Ord
import Data.Char 

main = do
    text <- readFile "input.txt"
    let layers = (chunksOf 6 (chunksOf 25 (digits ((lines text) !! 0))))
    mapM_ print $ map (\layer -> concat (map disp layer)) (process layers)

disp val 
    | val == 0 = " "
    | val == 1 = "*"
    | val == 2 = "?"

process layers = foldl (merge_layers) (head layers) (tail layers)

merge_layers a b = let 
    cmp layer_1 layer_2
        | layer_1 == 2 = layer_2
        | otherwise = layer_1
    in chunksOf 25 (zipWith cmp (concat a) (concat b)) 

digits n = map (\x -> read [x] :: Int) (n)