using ExRam.Gremlinq.Core;
using FSharp.IssueDemo;

namespace CSharp.IssueAbsent;

public class GremlinQueries
{
    public object DepartmentUsersQuery_WorksWithoutAnyLambdaTypeSpecification(
        IGremlinQuerySource gremlinQuerySource,
        Guid departmentId)
    {
        var resultQuery =
            gremlinQuerySource
                .V(departmentId)
                .OfType<Department>()
                .As((query, departmentStepLabel) =>
                    query
                        .Out<EdgeUserToDepartment>()
                        .OfType<User>()
                        .As((query, userStepLabel) =>
                            query
                                .Where(currentUser => currentUser.Name != "Admin")
                                .Select(userStepLabel)));

        return resultQuery;
    }
}