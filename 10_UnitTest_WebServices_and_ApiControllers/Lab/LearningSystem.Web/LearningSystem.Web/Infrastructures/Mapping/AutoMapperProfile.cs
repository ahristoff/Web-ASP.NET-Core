namespace LearningSystem.Web.Infrastructure.Mapping
{
    using AutoMapper;
    using LearningSystem.Common.Mapping;
    using System;
    using System.Linq;

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // witout REFLECTION
            //this.CreateMap<ApplicationUser, UserViewModel>()
            //    .ForMember(u => u.mailAddress, c =>c.MapFrom(u => u.Email));

            //with REFLECTION

            var allTypes = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .Where(a => a.GetName().Name.Contains("LearningSystem"))
                .SelectMany(a => a.GetTypes());

            var mappings = allTypes
                .Where(t => t.IsClass && !t.IsAbstract && t
                    .GetInterfaces()
                    .Where(i => i.IsGenericType)
                    .Select(i => i.GetGenericTypeDefinition())
                    .Contains(typeof(IMapFrom<>)))
                .Select(t => new
                {
                    //UserViewModel
                    Destination = t,
                    //ApplicationUser
                    Source = t
                    .GetInterfaces()
                    .Where(i => i.IsGenericType)
                    .Select(i => new
                    {
                        Definition = i.GetGenericTypeDefinition(),
                        Arguments = i.GetGenericArguments()
                    })
                    .Where(i => i.Definition == typeof(IMapFrom<>))
                    .SelectMany(i => i.Arguments)
                    .First()
                })
                .ToList();

            foreach (var mapping in mappings)
            {
                this.CreateMap(mapping.Source, mapping.Destination);
            }

            var customMappings = allTypes
                .Where(t => t.IsClass && !t.IsAbstract && typeof(IHaveCustomMapping).IsAssignableFrom(t))
                .Select(Activator.CreateInstance)
                .Cast<IHaveCustomMapping>()
                .ToList();

            foreach (var customMapping in customMappings)
            {
                customMapping.ConfigureMapping(this);
            }
        }
    }
}