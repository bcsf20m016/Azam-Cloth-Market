using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IRepo
{
    public interface ITagTypeRepo
    {
        public Task<int> createTagType(TagTypeDTO tag);
        public Task<int> deleteTagType(int id);
        public Task<int> updateTagType(TagTypeDTO tag);
        public Task<IEnumerable<TagTypeDTO>> GetAllTagsType();
        public Task<TagTypeDTO> GetTagType(int id);
        public int GetTagType(string name);
        public string GetTagTypeByName(int typeID);
    }
}
