using GraphQL.Types;
using ReactAdvantage.Api.GraphQLSchema.Models;

namespace ReactAdvantage.Api.GraphQLSchema.Types
{
    public class UserType : ObjectGraphType<UserDto>
    {
        public UserType()
        {
            Field(x => x.Id).Description("The Id of the User.");
            Field(x => x.FirstName, nullable: true).Description("The first name of the User.");
            Field(x => x.LastName, nullable: true).Description("The last name of the User.");
            Field(x => x.UserName, nullable: true).Description("The system username of the User.");
            Field(x => x.Email, nullable: true).Description("The email address of the User.");
            Field(x => x.IsActive, nullable: false).Description("True if the user is active.");
            Field(x => x.Roles, nullable: true, type: typeof(ListGraphType<StringGraphType>)).Description("The list of roles assigned to the User.");
        }
    }
}
