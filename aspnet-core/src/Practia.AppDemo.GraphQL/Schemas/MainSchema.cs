using Abp.Dependency;
using GraphQL;
using GraphQL.Types;
using Practia.AppDemo.Queries.Container;

namespace Practia.AppDemo.Schemas
{
    public class MainSchema : Schema, ITransientDependency
    {
        public MainSchema(IDependencyResolver resolver) :
            base(resolver)
        {
            Query = resolver.Resolve<QueryContainer>();
        }
    }
}