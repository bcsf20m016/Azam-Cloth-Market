using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IRepo
{
    public interface ITagRepo
    {
        public Task<int> createTag(TagDTO tag);
        public Task<int> deleteTag(int id);
        public Task<int> updateTag(TagDTO tag);
        public Task<IEnumerable<TagDTO>> getAllTags();
        public Task<IEnumerable<TagDTO>> getAllTagsByStoreId(int storeId);
        public Task<TagDTO> getTag(int id);
        public Task<TagDTO> getTagByStoreId(int id, int storeId);
        public Task<IEnumerable<TagDTO>> getTypeTags(int typeID);
        public Task<TagDTO> getTag(string tagName);
        public Task<IEnumerable<TagDTO> >getAllTagsWithSpecificType(int typeId);
       
    }
}
