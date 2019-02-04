// ts2fable 0.6.1
module rec GenerateUrls

open System
open Fable.Core
open Fable.Import.JS
open MPathToRegexp
open MUniversalRouter

type PathFunctionOptions = PathToRegexp.PathFunctionOptions
type Params = MParams

type [<AllowNullLiteral>] IExports =
    abstract generateUrls: router: UniversalRouter * ?options: GenerateUrlsOptions -> (string -> Params -> string)

type [<AllowNullLiteral>] GenerateUrlsOptions =
    inherit PathFunctionOptions
    abstract stringifyQueryParams: (Params -> string) option with get, set
