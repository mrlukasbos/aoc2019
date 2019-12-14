import Data.List
import qualified Data.List.Split as Split
import Data.Ord
import Data.Maybe
import qualified Data.Map.Strict as Map
import Debug.Trace
import Data.Char (isSpace)

data Conversion = Conversion {
    ingredients :: [(String, Int)],
    result :: (String, Int)
} deriving (Show, Eq)

-- The answer of a is logged in the answer for b: it is the amount of blocks in the first iteration.
day_14a :: IO ()
day_14a = do
    text <- readFile "input.txt"
    let split_1 = map (map (Split.splitOn ", ")) (map (Split.splitOn " => ") (lines text))
    let split_2 = map (map item_to_tup) (map (concat) split_1)
    let conversions = map to_conversion split_2
    let fuel_conv = get_conversion_by_result_str "FUEL" conversions
    print $ calc conversions (Map.fromList (ingredients (fromJust fuel_conv)))

to_conversion :: [(String, Int)] -> Conversion
to_conversion items = Conversion {
    ingredients = init items, 
    result = last items 
}

item_to_tup item = (trim (s !! 1), read (s !! 0) :: Int) where s = Split.splitOn " " item

-- Get the conversion for a result
get_conversion_by_result_str :: String -> [Conversion] -> Maybe Conversion
get_conversion_by_result_str str conversions 
    | filtered_conversions == [] = Nothing
    | otherwise = Just $ filtered_conversions !! 0
    where filtered_conversions = filter (\conv -> str == fst (result conv)) conversions

-- Return true if the ingredient 'str' exists
has_ingredient_str :: String -> Conversion -> Bool
has_ingredient_str str conversion  = (filter (\(name, _) -> name == str) (ingredients conversion)) /= []

-- increase the factor of conversions until 'result' satisfies 'amount'
get_requested_conversion :: Conversion -> Int -> Int -> Conversion
get_requested_conversion conversion factor amount
    | (snd (result new_conversion)) >= amount = new_conversion
    | otherwise = get_requested_conversion conversion (factor + 1) amount
        where new_conversion = scale_conversion conversion factor 

-- Scale a conversion with a given factor
scale_conversion :: Conversion -> Int -> Conversion
scale_conversion conversion factor = conversion {
    ingredients = map (scale_item factor) (ingredients conversion),
    result = scale_item factor (result conversion)
}
scale_item :: Int -> (String, Int) -> (String, Int)
scale_item factor (a,b) = (a, b * factor) 

calc :: [Conversion] -> Map.Map String Int -> Int
calc all_conversions requested_ingredients 
    | length ingredient_list == length (filter (\ing -> ("ORE" == (fst ing))) ingredient_list) = sum (map snd ingredient_list) -- if we only need ore return the sum of requested ore
    | otherwise = trace (show ingredient_list) $ calc all_conversions newly_requested_ingredients
        where 
            ingredient_list =  (Map.toList requested_ingredients)
            requested_conversions = map (\ingredient -> get_requested_conversion (fromJust (get_conversion_by_result_str (fst ingredient) all_conversions)) 1 (snd ingredient)) ingredient_list
            newly_requested_ingredients = Map.fromListWith (+) (concat (map (ingredients) requested_conversions))

trim :: String -> String
trim = f . f where f = reverse . dropWhile isSpace