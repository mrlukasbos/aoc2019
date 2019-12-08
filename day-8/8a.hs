import Data.List.Split
import Data.List
import Data.Ord

main = do
    text <- readFile "input.txt"
    let layers = (chunksOf 6 (chunksOf 25 (digits ((lines text) !! 0))))
    let least_zeros_layer = (sortBy (comparing (count 0)) layers) !! 0
    print $ (count (1) least_zeros_layer) * (count (2) least_zeros_layer)
        
count val layer = sum $ map (\n -> length (filter (== val) n)) layer

digits n = map (\x -> read [x] :: Int) (n)