// ts2fable 0.6.1
module rec MUniversalRouter

open System
open Fable.Core
open Fable.Import.JS
open MPathToRegexp

type [<AllowNullLiteral>] IExports =
    abstract UniversalRouter: UniversalRouterStatic

type [<AllowNullLiteral>] Params =
    [<Emit "$0[$1]{{=$2}}">] abstract Item: paramName: string -> obj option with get, set

type MParams = Params

type [<AllowNullLiteral>] Context =
    [<Emit "$0[$1]{{=$2}}">] abstract Item: propName: string -> obj option with get, set

type [<AllowNullLiteral>] ResolveContext =
    inherit Context
    abstract pathname: string with get, set

type RouteContext<'R> =
    RouteContext<obj, 'R>

type [<AllowNullLiteral>] RouteContext<'C, 'R> =
    inherit ResolveContext
    abstract router: UniversalRouter<'C, 'R> with get, set
    abstract route: Route with get, set
    abstract baseUrl: string with get, set
    abstract path: string with get, set
    abstract ``params``: Params with get, set
    abstract keys: ResizeArray<PathToRegexp.Key> with get, set
    abstract next: (bool -> Promise<'R>) with get, set

type Route<'R> =
    Route<obj, 'R>

type Route =
    Route<obj, obj>

type [<AllowNullLiteral>] Route<'C, 'R> =
    abstract path: U3<string, RegExp, Array<U2<string, RegExp>>> option with get, set
    abstract name: string option with get, set
    abstract parent: Route option with get, set
    abstract children: Routes<'C, 'R> option with get, set
    abstract action: (obj -> Params -> U3<'R, Promise<'R>, unit>) option with get, set

type Routes<'R> =
    Routes<obj, 'R>

type Routes =
    Routes<obj, obj>

type Routes<'C, 'R> =
    Array<Route<'C, 'R>>

type Options<'R> =
    Options<obj, 'R>

type Options =
    Options<obj, obj>

type [<AllowNullLiteral>] Options<'C, 'R> =
    abstract context: 'C option with get, set
    abstract baseUrl: string option with get, set
    abstract resolveRoute: (obj -> Params -> obj option) option with get, set
    abstract errorHandler: (obj -> obj -> obj option) option with get, set

type UniversalRouter<'R> =
    UniversalRouter<obj, 'R>

type UniversalRouter =
    UniversalRouter<obj, obj>

type [<AllowNullLiteral>] UniversalRouter<'C, 'R> =
    abstract pathToRegexp: obj with get, set
    abstract resolve: pathnameOrContext: U2<string, ResolveContext> -> Promise<'R>

type [<AllowNullLiteral>] UniversalRouterStatic =
    [<Emit "new $0($1...)">] abstract Create: routes: U2<Route<'C, 'R>, Routes<'C, 'R>> * ?options: Options<'C> -> UniversalRouter<'C, 'R>

    module GenerateUrls =
      type PathFunctionOptions = PathToRegexp.PathFunctionOptions
      type Params = MParams

      type [<AllowNullLiteral>] IExports =
          abstract generateUrls: router: UniversalRouter * ?options: GenerateUrlsOptions -> (string -> Params -> string)

      type [<AllowNullLiteral>] GenerateUrlsOptions =
          inherit PathFunctionOptions
          abstract stringifyQueryParams: (Params -> string) option with get, set    

let [<Import("*","module")>] universalRouter: IExports = jsNative
