// ts2fable 0.6.1
module rec MPathToRegexp

open System
open Fable.Core
open Fable.Import.JS

let [<Import("*","module")>] pathToRegexp: PathToRegexp.IExports = jsNative

type [<AllowNullLiteral>] IExports =
    abstract pathToRegexp: path: PathToRegexp.Path * ?keys: ResizeArray<PathToRegexp.Key> * ?options: obj -> RegExp

module PathToRegexp =

    type [<AllowNullLiteral>] IExports =
        /// Parse an Express-style path into an array of tokens.
        abstract parse: path: string * ?options: ParseOptions -> ResizeArray<Token>
        /// Transforming an Express-style path into a valid path.
        abstract compile: path: string * ?options: ParseOptions -> PathFunction<'P>
        /// Transform an array of tokens into a path generator function.
        abstract tokensToFunction: tokens: ResizeArray<Token> -> PathFunction<'P>
        /// Transform an array of tokens into a matching regular expression.
        abstract tokensToRegExp: tokens: ResizeArray<Token> * ?keys: ResizeArray<Key> * ?options: RegExpOptions -> RegExp

    type [<AllowNullLiteral>] RegExpOptions =
        /// When `true` the regexp will be case sensitive. (default: `false`)
        abstract sensitive: bool option with get, set
        /// When `true` the regexp allows an optional trailing delimiter to match. (default: `false`)
        abstract strict: bool option with get, set
        /// When `true` the regexp will match to the end of the string. (default: `true`)
        abstract ``end``: bool option with get, set
        /// When `true` the regexp will match from the beginning of the string. (default: `true`)
        abstract start: bool option with get, set
        /// Sets the final character for non-ending optimistic matches. (default: `/`)
        abstract delimiter: string option with get, set
        /// List of characters that can also be "end" characters.
        abstract endsWith: U2<string, ResizeArray<string>> option with get, set
        /// List of characters to consider delimiters when parsing. (default: `undefined`, any character)
        abstract whitelist: U2<string, ResizeArray<string>> option with get, set

    type [<AllowNullLiteral>] ParseOptions =
        /// Set the default delimiter for repeat parameters. (default: `'/'`)
        abstract delimiter: string option with get, set

    type [<AllowNullLiteral>] Key =
        abstract name: U2<string, float> with get, set
        abstract prefix: string with get, set
        abstract delimiter: string with get, set
        abstract optional: bool with get, set
        abstract repeat: bool with get, set
        abstract pattern: string with get, set

    type [<AllowNullLiteral>] PathFunctionOptions =
        /// Function for encoding input strings for output.
        abstract encode: (string -> Key -> string) option with get, set

    type Token =
        U2<string, Key>

    [<RequireQualifiedAccess; CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
    module Token =
        let ofString v: Token = v |> U2.Case1
        let isString (v: Token) = match v with U2.Case1 _ -> true | _ -> false
        let asString (v: Token) = match v with U2.Case1 o -> Some o | _ -> None
        let ofKey v: Token = v |> U2.Case2
        let isKey (v: Token) = match v with U2.Case2 _ -> true | _ -> false
        let asKey (v: Token) = match v with U2.Case2 o -> Some o | _ -> None

    type Path =
        U3<string, RegExp, Array<U2<string, RegExp>>>

    [<RequireQualifiedAccess; CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
    module Path =
        let ofString v: Path = v |> U3.Case1
        let isString (v: Path) = match v with U3.Case1 _ -> true | _ -> false
        let asString (v: Path) = match v with U3.Case1 o -> Some o | _ -> None
        let ofRegExp v: Path = v |> U3.Case2
        let isRegExp (v: Path) = match v with U3.Case2 _ -> true | _ -> false
        let asRegExp (v: Path) = match v with U3.Case2 o -> Some o | _ -> None
        let ofArray v: Path = v |> U3.Case3
        let isArray (v: Path) = match v with U3.Case3 _ -> true | _ -> false
        let asArray (v: Path) = match v with U3.Case3 o -> Some o | _ -> None

    type PathFunction =
        PathFunction<obj>

    type [<AllowNullLiteral>] PathFunction<'P> =
        [<Emit "$0($1...)">] abstract Invoke: ?data: 'P * ?options: PathFunctionOptions -> string
