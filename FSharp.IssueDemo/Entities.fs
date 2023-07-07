namespace FSharp.IssueDemo

open System

[<AbstractClass>]
type Vertex<'TId> (id: 'TId) =
    member val Id = id with get, set
    member val Label = "" with get, set


[<AbstractClass>]
type Edge () =
    member val Id: obj = Unchecked.defaultof<_> with get, set
    member val Label: string = "" with get, set
    

type User (id: Guid, name: string) =
    inherit Vertex<Guid> (id)
    member _.Name = name


type Department (id: Guid, name: string, organizationId: Guid) =
    inherit Vertex<Guid> (id)
    member _.Name = name
    member _.OrganizationId = organizationId
    

type EdgeUserToDepartment () =
    inherit Edge ()

