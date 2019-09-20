using ControleVeiculos.Domain.Command.Groups;
using ControleVeiculos.Domain.Entities.Groups;
using System.Collections.Generic;


namespace ControleVeiculos.Domain.Repositories
{
    public interface IGroupRepository
    {
        void Add(Group group);
        void Update(Group group);
        Group GetByID(int groupID);
        List<Group> GetAll(FilterGroupCommand command);
        List<Group> GetAll(int groupID);
        void Delete(int groupID);
        string GetGroupNameByID(int groupID);

    }
}

