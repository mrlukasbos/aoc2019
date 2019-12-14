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

day_14b :: IO ()
day_14b = do
    text <- readFile "input.txt"
    let split_1 = map (map (Split.splitOn ", ")) (map (Split.splitOn " => ") (lines text))
    let split_2 = map (map item_to_tup) (map (concat) split_1)
    let conversions = map to_conversion split_2
    print $ brute_force conversions 0 1000000000000 -- calc conversions (Map.fromList (ingredients fuel_conv)) Map.empty

brute_force conversions attempt factor
    | result >= 1000000000000 && factor > 1 = brute_force conversions (attempt - (factor `div` 2)) (factor `div` 2)
    | result >= 1000000000000 = attempt - 1
    | otherwise = brute_force conversions (attempt + factor) factor
        where 
            fuel_conv = scale_conversion (fromJust (get_conversion_by_result_str "FUEL" conversions)) attempt
            result = calc conversions (Map.fromList (ingredients fuel_conv)) Map.empty

day_14a :: IO ()
day_14a = do
    text <- readFile "input.txt"
    let split_1 = map (map (Split.splitOn ", ")) (map (Split.splitOn " => ") (lines text))
    let split_2 = map (map item_to_tup) (map (concat) split_1)
    let conversions = map to_conversion split_2
    let fuel_conv = get_conversion_by_result_str "FUEL" conversions
    print $ calc conversions (Map.fromList (ingredients (fromJust fuel_conv))) Map.empty
    

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
    | new_val >= amount = new_conversion
    | otherwise = get_requested_conversion conversion (factor + increase) amount
        where 
            new_conversion = scale_conversion conversion factor 
            new_val = (snd (result new_conversion))
            increase
                | ((amount-new_val) `div` new_val) <= 0 = 1
                | otherwise = (amount `div` new_val) - 1


-- Scale a conversion with a given factor
scale_conversion :: Conversion -> Int -> Conversion
scale_conversion conversion factor = conversion {
    ingredients = map (scale_item factor) (ingredients conversion),
    result = scale_item factor (result conversion)
}

-- scales 
scale_item :: Int -> (String, Int) -> (String, Int)
scale_item factor (a,b) = (a, b * factor) 

calc :: [Conversion] -> Map.Map String Int -> Map.Map String Int -> Int
calc all_conversions requested_ingredients leftovers = let
    
    filtered_requests = Map.filterWithKey (\k _ -> k /= "ORE" ) requested_ingredients -- get all entries that are not ore
    ore_requests = Map.filterWithKey (\k _ -> k == "ORE" ) requested_ingredients -- get all entries that are ore

    filtered_requests_list = Map.toList filtered_requests
    ore_requests_list = Map.toList ore_requests

    new_status = check_ingredient (head filtered_requests_list) leftovers all_conversions
    new_requested_ingredients = Map.fromListWith (+) ((snd new_status) ++ (tail filtered_requests_list) ++ (ore_requests_list)) -- the ore needs to be put back in
    new_leftovers = (fst new_status)

    exec 
        | filtered_requests_list == [] = sum (map snd ore_requests_list) 
        | otherwise = {-trace ("requested: " ++ show new_requested_ingredients ++ "-------  leftovers: " ++ (show leftovers)) $ -} calc all_conversions new_requested_ingredients new_leftovers
    in exec

check_ingredient :: (String, Int) -> Map.Map String Int -> [Conversion] -> (Map.Map String Int, [(String, Int)])
check_ingredient ingredient leftovers conversions
        | diff_with_leftover >= 0 = (Map.insert (fst ingredient) diff_with_leftover leftovers, [])
        | otherwise = (Map.insert (fst ingredient) (new_leftover_value) leftovers, (ingredients conversion_to_get_ingredient))
        where 
            leftovers_for_key = (Map.findWithDefault 0 (fst ingredient) leftovers)
            diff_with_leftover = leftovers_for_key - snd (ingredient)
            basic_conversion_to_get_ingredient = fromJust $ get_conversion_by_result_str (fst ingredient) conversions
            conversion_to_get_ingredient = get_requested_conversion basic_conversion_to_get_ingredient 1 ((snd ingredient) - leftovers_for_key)
            new_leftover_value = leftovers_for_key + (snd (result conversion_to_get_ingredient)) - (snd ingredient)

trim :: String -> String
trim = f . f where f = reverse . dropWhile isSpace