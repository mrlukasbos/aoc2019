import Data.List

toInt :: [String] -> [Int]
toInt = map read

main = do
    text <- readFile "./input.txt"
    let input_list = toInt $ lines text
    print $ "Part one"
    print $ head [a*b | a <- input_list, b <- input_list, a + b == 2020]
    print $ "Part two"
    print $ head [a*b*c | a <- input_list, b <- input_list,  c <- input_list, a + b + c == 2020]