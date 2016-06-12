using Dapper;
using EHome.Storage.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using EHome.Storage.ViewModels;
using System.Collections.ObjectModel;
using System.Linq;
using System;

namespace EHome.Storage
{
    public partial class EHomeService : IEHomeService
    {
        private readonly IDbConnectionSelection _dbConnectionSelection;

        public EHomeService(IDbConnectionSelection dbConnectionSelection)
        {
            _dbConnectionSelection = dbConnectionSelection;
        }

        public async Task<IEnumerable<DeviceModel>> GetDevicesAsync()
        {
            using (var connection = await _dbConnectionSelection.OpenDbConnectionAsync())
            {
                var result = await connection.QueryAsync<DeviceModel>(@"select * from Devices");
                return result;
            }
        }

        public async Task UpdateDeviceStateAsync(int deviceId, string value)
        {
            using (var connection = await _dbConnectionSelection.OpenDbConnectionAsync())
            {
                await connection.ExecuteAsync("update DeviceStates set value = @Value where DeviceId = @DeviceId", new { DeviceId = deviceId, Value = value });
            }
        }

        public async Task<IEnumerable<ModuleModel>> GetModulesAsync()
        {
            using (var connection = await _dbConnectionSelection.OpenDbConnectionAsync())
            {
                var result = await connection.QueryAsync<ModuleModel>(@"select * from Modules");
                return result;
            }
        }

        public async Task<IEnumerable<GroupDeviceViewModel>> GetGroupDevicesAsync()
        {
            using (var connection = await _dbConnectionSelection.OpenDbConnectionAsync())
            {
                var sql = @"select * from Devices;
                            select * from DeviceStates;
                            select * from GroupDevices;
                            select * from Groups;
                            select * from Modules;
                            select * from Properties;";
                var query = await connection.QueryMultipleAsync(sql);

                var devices = await query.ReadAsync<DeviceModel>();
                var states = await query.ReadAsync<DeviceStateModel>();
                var groupDevices = await query.ReadAsync<GroupDeviceModel>();
                var groups = await query.ReadAsync<GroupModel>();
                var modules = await query.ReadAsync<ModuleModel>();
                var properties = await query.ReadAsync<PropertyModel>();

                var result = new Collection<GroupDeviceViewModel>();
                foreach (var group in groups)
                {
                    var vm = new GroupDeviceViewModel
                    {
                        Id = group.Id,
                        Code = group.Code,
                        Description = group.Description
                    };

                    foreach (var device in devices)
                    {
                        var md = modules.Single(c => c.Id == device.ModuleId);
                        var propertyIds = device.Properties.Split(',').Select(int.Parse);
                        var deviceProperties = properties.Where(c => propertyIds.Contains(c.Id));

                        var dvm = new DeviceViewModel
                        {
                            Id = device.Id,
                            Code = device.Code
                        };

                        foreach(var property in deviceProperties)
                        {
                            var selectedState = states.FirstOrDefault(c => c.DeviceId == device.Id && c.PropertyId == property.Id);
                            dvm.States.Add(new DeviceStateViewModel
                            {
                                Id = property.Id,
                                PropertyType = property.Type,
                                Property = property.Description,
                                Value = selectedState == null ? null : selectedState.Value
                            });
                        }
                        vm.Devices.Add(dvm);
                    }

                    result.Add(vm);
                }

                return result;
            }
        }
    }
}
