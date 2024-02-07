using AutoMapper;
using Business.IRepo;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class TagRepo : ITagRepo
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public TagRepo(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<int> createTag(TagDTO tag)
        {
            if(tag.Id !=0)
            {
                var oldTag = _db.tags.FirstOrDefault(i => i.Id == tag.Id);
                oldTag.Name = tag.Name;
                return await _db.SaveChangesAsync();
            }
            Tag newTag = _mapper.Map<TagDTO, Tag>(tag);
            await _db.tags.AddAsync(newTag);
            return await _db.SaveChangesAsync();
        }

        public async Task<int> deleteTag(int id)
        {
            Tag tag = await _db.tags.FindAsync(id);
            if(tag!=null)
            {
                _db.tags.Remove(tag);
                return _db.SaveChanges();
            }
            return 0;
        }

        public async Task<IEnumerable<TagDTO>> getAllTags()
        {
            return _mapper.Map<IEnumerable<Tag>, IEnumerable<TagDTO>>(_db.tags);
        }
        public async Task<IEnumerable<TagDTO>> getTypeTags(int typeID)
        {
            return _mapper.Map<IEnumerable<Tag>, IEnumerable<TagDTO>>(_db.tags.Where(i=>i.tagTypeId == typeID).ToList());
        }

        public async Task<IEnumerable<TagDTO>> getAllTagsByStoreId(int storeId)
        {
            return _mapper.Map<IEnumerable<Tag>, IEnumerable<TagDTO>>(_db.tags.Where(i => i.storeId == storeId).ToList());
        }

        public async Task<TagDTO> getTag(int id)
        {
            return _mapper.Map<Tag, TagDTO>(await _db.tags.FirstOrDefaultAsync(i => i.Id == id));
        }
        public async Task<TagDTO> getTag(string tagName)
        {
            return _mapper.Map<Tag, TagDTO>(await _db.tags.FirstOrDefaultAsync(i => i.Name.ToLower() == tagName.ToLower()));
        }

        public async Task<TagDTO> getTagByStoreId(int id, int storeId)
        {
            return _mapper.Map<Tag, TagDTO>(await _db.tags.FirstOrDefaultAsync(i => i.Id == id && i.storeId == storeId));
        }

        public async Task<IEnumerable<TagDTO>> getAllTagsWithSpecificType(int typeId)
        {
            return _mapper.Map<IEnumerable<Tag>, IEnumerable<TagDTO>>(_db.tags.Where(i => i.tagTypeId == typeId).ToList());
        }

        
        public async Task<int> updateTag(TagDTO tag)
        {
            Tag oldTag = await _db.tags.FindAsync(tag.Id);
            Tag newTag = _mapper.Map<TagDTO, Tag>(tag, oldTag);
            _db.tags.Update(newTag);
            return await _db.SaveChangesAsync();
        }

        
    }
}
