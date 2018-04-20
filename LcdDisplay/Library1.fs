namespace LcdDisplay

module Parser =
    open System

    let lineCount = 3
    let digitLines = [
        " _     _  _     _  _  _  _  _ "
        "| | |  _| _||_||_ |_   ||_||_|"
        "|_| | |_  _|  | _||_|  ||_| _|"
    ]
    let chunkedDigitLines = 
        digitLines 
        |> List.map (Seq.chunkBySize lineCount >> Array.ofSeq) 
        |> Seq.map ((Seq.map String) >> Array.ofSeq)
        |> Array.ofSeq
        
    let toDigits (n:int) = 
        n.ToString() 
        |> Seq.map (string >> Int32.Parse) 
        |> List.ofSeq

    let genLine ln =
        List.fold (fun l d -> l+chunkedDigitLines.[ln].[d]) ""

    let lineGenerators =
        [0..lineCount-1] |> List.map genLine
            
    let getLcdLines digits =
        lineGenerators |> List.map ((|>) digits)
    
    let getLcdLinesNumber = toDigits >> getLcdLines
    let print = printfn "%s"
    let printLcd = getLcdLinesNumber >> List.iter print

