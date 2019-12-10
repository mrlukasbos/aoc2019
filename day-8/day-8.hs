import Data.List.Split
import Data.List
import Data.Ord
import Data.Char 

-- Solution A
day_8a = do
    text <- readFile "input.txt"
    let least_zeros_layer = (sortBy (comparing (count 0)) (layers ((lines text) !! 0))) !! 0
    print $ (count (1) least_zeros_layer) * (count (2) least_zeros_layer) where 
        count val layer = sum $ map (\n -> length (filter (== val) n)) layer

layers txt =  (chunksOf 6 (chunksOf 25 (digits txt)))

-- Solution B
day_8b = do
    text <- readFile "input.txt"
    mapM_ print $ map (\layer -> concat (map disp layer)) (process (layers ((lines text) !! 0)))

disp val 
    | val == 0 = " "
    | val == 1 = "#"
    | val == 2 = "?"

process layers = foldl (merge_layers) (head layers) (tail layers)

merge_layers a b = let 
    cmp layer_1 layer_2
        | layer_1 == 2 = layer_2
        | otherwise = layer_1
    in zipWith (zipWith cmp) a b

digits n = map (\x -> read [x] :: Int) (n)