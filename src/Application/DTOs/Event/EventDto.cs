namespace EventSystem.Application.DTOs.Event;

using AutoMapper;
using EventSystem.Application.Common.Mappings;
using EventSystem.Application.EventManagement.Commands.CreateEventCommand;
using EventSystem.Domain.Entities;


public class EventDto : IMapFrom<Event>
{


    void IMapFrom<Event>.Mapping(Profile profile)
    {
        profile.CreateMap<EventDto, Event>().ReverseMap();
        profile.CreateMap<CreateEventCommand, EventDto>().ReverseMap();
    }
}
