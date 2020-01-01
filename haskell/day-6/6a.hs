import Debug.Trace
main = do
    text <- readFile "input.txt"
    let input_list = map separate $ lines text
    print $ traverse_tree input_list 0 0 (find_by_key "COM" input_list)

separate str = (lst !! 0, lst !! 1) where lst = split ')' str

traverse_tree objs depth total items 
    | items == [] = total
    | otherwise = trace (show items) $ traverse_tree objs (depth+1) (total + ((depth+1) * (length items))) all_children
        where all_children = find_children_multiple_parents items objs

find_children_multiple_parents parents objs = concat (map (\children -> find_children children objs) parents)

-- gets list of tuples where snd obj == fst
find_children obj objs = find_by_key (snd obj) objs
 
-- gets list of tuples where fst == key
find_by_key key objs = filter ((== key) . fst) objs

-- -- split each element from the string
split :: Eq a => a -> [a] -> [[a]]
split d [] = []
split d s = x : split d (drop 1 y) where (x,y) = span (/= d) s
