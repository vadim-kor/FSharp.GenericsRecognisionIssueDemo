namespace FSharp.IssueDemo

open System
open ExRam.Gremlinq.Core

module GremlinQueries =

    // This method supposed to work even without specifying the types of the lambda function parameters
    // It perfectly works in C#, but not in F#.
    // See the 'GetDepartmentUsers_CompilableVersion' method to find the changes that make it work
    let GetDepartmentUsersQuery_UncompilableButValidVersion
        (gremlinQuerySource: IGremlinQuerySource)
        (departmentId: Guid)
        =
        let resultQuery =
            gremlinQuerySource
                .V(departmentId)
                .OfType<Department>()
                .As (fun query departmentStepLabel ->
                    query
                        .Out<EdgeUserToDepartment>()
                        .OfType<User>()
                        .As (fun query userStepLabel ->
                            query
                                .Where(fun currentUser -> currentUser.Name <> "Admin")
                                .Select (userStepLabel)))

        resultQuery


    // Same implementation but with some types specified explicitly to make it compilable.
    // We have to specify the second parameter type of the lambda function passed to 'As' method
    // Also we have to specify the type of the lambda function parameter passed to 'Where' method
    // 'Where' is not a linq method but a method of the 'IGremlinQuery' interface
    let GetDepartmentUsers_CompilableVersion (gremlinQuerySource: IGremlinQuerySource) (departmentId: Guid) =
        let resultQuery =
            gremlinQuerySource
                .V(departmentId)
                .OfType<Department>()
                // It doesn't compile without specifying the type of 'departmentStepLabel'
                .As (fun query (departmentStepLabel: StepLabel<_, Department>) ->
                    query
                        .Out<EdgeUserToDepartment>()
                        .OfType<User>()
                        // It doesn't compile without specifying the type of 'userStepLabel':
                        .As (fun query (userStepLabel: StepLabel<_, User>) ->
                            query
                                // It doesn't compile without specifying the type of 'currentUser':
                                .Where(fun (currentUser: User) -> currentUser.Name <> "Admin")
                                .Select userStepLabel))

        resultQuery
