using AutoMapper;

//Tip: Reason is because, UseValue is a static,
//so it’s set once when the MapProfile is instantiated and all subsequent .Map()
//invokes will use the same static value. Hence the sticky time value
namespace Models.CustomMapping
{



    public class UserCustomMapping : IHaveCustomMapping
    {
        public void CreateMappings(Profile profile)
        {
        }
    }
}
