(*If we list all the natural numbers below 10 that are multiples of 3 or 5, we get 3, 5, 6 and 9. The sum of these multiples is 23.
Find the sum of all the multiples of 3 or 5 below 1000.*)
let sumOfAllMultiplesOf3And5Below n = 
    let upTo1000 = seq { 1 .. n - 1 }
    upTo1000
    |> Seq.filter (fun x -> (x%3) = 0 || (x%5) = 0)
    |> Seq.reduce(+)

sumOfAllMultiplesOf3And5Below 1000 |> printfn "Problem 1 solution is %d"