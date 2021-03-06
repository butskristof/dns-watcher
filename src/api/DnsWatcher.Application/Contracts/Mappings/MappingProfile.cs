﻿using System;
using System.Linq;
using System.Reflection;
using AutoMapper;
using DnsWatcher.Application.Contracts.Data.Domains;
using DnsWatcher.Application.Contracts.Data.Servers;
using DnsWatcher.Domain.Entities.Domains;
using DnsWatcher.Domain.Entities.Servers;

namespace DnsWatcher.Application.Contracts.Mappings
{
	internal class MappingProfile : Profile
	{
		public MappingProfile()
		{
			ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());

			CreateMap<CreateDnsServerData, DnsServer>();
			CreateMap<UpdateDnsServerData, DnsServer>();
			CreateMap<CreateWatchedDomainData, WatchedDomain>();
			CreateMap<UpdateWatchedDomainData, WatchedDomain>();
			CreateMap<CreateWatchedRecordData, WatchedRecord>();
			CreateMap<UpdateWatchedRecordData, WatchedRecord>();
		}

		private void ApplyMappingsFromAssembly(Assembly assembly)
		{
			var types = assembly.GetExportedTypes()
				.Where(t => t.GetInterfaces().Any(i =>
					i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
				.ToList();

			foreach (var type in types)
			{
				var instance = Activator.CreateInstance(type);

				var methodInfo = type.GetMethod("Mapping")
								?? type.GetInterface("IMapFrom`1").GetMethod("Mapping");

				methodInfo?.Invoke(instance, new object[] {this});
			}
		}
	}
}