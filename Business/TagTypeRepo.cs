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
    public class TagTypeRepo : ITagTypeRepo
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public TagTypeRepo(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
    
        public async Task<int> createTagType(TagTypeDTO tag)
        {
            if(tag.id!=0)
            {
                var oldTag = _db.tagTypes.FirstOrDefault(i => i.id == tag.id);
                oldTag.name = tag.name;
                return await _db.SaveChangesAsync();
            }
            TagType newTag = _mapper.Map<TagTypeDTO, TagType>(tag);
            var searchTag = _db.tagTypes.FirstOrDefault(i => i.name == tag.name);
            if(searchTag == null)
            {
                await _db.tagTypes.AddAsync(newTag);
            }
            
            return await _db.SaveChangesAsync();
        }

        public async Task<int> deleteTagType(int id)
        {
            TagType tag = await _db.tagTypes.FindAsync(id);
            if(tag!=null)
            {
                _db.tagTypes.Remove(tag);
                return _db.SaveChanges();
            }
            return 0;
        }

        public async Task<IEnumerable<TagTypeDTO>> GetAllTagsType()
        {
            return _mapper.Map<IEnumerable<TagType>, IEnumerable<TagTypeDTO>>(_db.tagTypes);
        }

        public async Task<TagTypeDTO> GetTagType(int id)
        {
            return _mapper.Map<TagType, TagTypeDTO>(await _db.tagTypes.FirstOrDefaultAsync(i => i.id == id));
        }
        
        public int GetTagType(string name)
        {
            var tagType = _db.tagTypes.FirstOrDefault(i => i.name == name);
            return tagType.id;
        }
        public string GetTagTypeByName(int typeID)
        {
            var tagType = _db.tagTypes.FirstOrDefault(i => i.id == typeID);
            return tagType.name;
        }

        public async Task<int> updateTagType(TagTypeDTO tag)
        {
            TagType oldTag = await _db.tagTypes.FindAsync(tag.id);
            TagType newTag = _mapper.Map<TagTypeDTO, TagType>(tag, oldTag);
            _db.tagTypes.Update(newTag);
            return await _db.SaveChangesAsync();
        }
    }
}
