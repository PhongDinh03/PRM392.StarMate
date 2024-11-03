using Application.IRepository;
using Application.IService;
using Application.ServiceResponse;
using Application.ViewModels.LikeZodiacDTO;
using AutoMapper;
using Domain.Models;

namespace Application.Service
{
    public class LikeZodiacService : ILikeZodiacService
    {
        private readonly ILikeZodiacRepo _repository;
        private readonly IMapper _mapper;

        public LikeZodiacService(ILikeZodiacRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<LikeZodiacDTO>> AddLikeZodiacAsync(CreateLikeZodiacDTO createDto)
        {
            var response = new ServiceResponse<LikeZodiacDTO>();
            try
            {
                var likeZodiac = _mapper.Map<LikeZodiac>(createDto);
                await _repository.AddAsync(likeZodiac);
                response.Data = _mapper.Map<LikeZodiacDTO>(likeZodiac);
                response.Success = true;
                response.Message = "LikeZodiac created successfully";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<string>> DeleteLikeZodiacAsync(int id)
        {
            var response = new ServiceResponse<string>();
            try
            {
                var likeZodiac = await _repository.GetByIdAsync(id);
                if (likeZodiac == null)
                {
                    response.Success = false;
                    response.Message = "LikeZodiac not found";
                    return response;
                }
                await _repository.Remove(likeZodiac);
                response.Success = true;
                response.Message = "LikeZodiac deleted successfully";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<List<LikeZodiacDTO>>> GetAllLikeZodiac()
        {
            var response = new ServiceResponse<List<LikeZodiacDTO>>();
            try
            {
                var likeZodiacs = await _repository.GetListLikeZodiacAsync();
                response.Data = likeZodiacs.Select(l => _mapper.Map<LikeZodiacDTO>(l)).ToList();
                response.Success = true;
                response.Message = "Get all LikeZodiac successfully";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<LikeZodiacDTO?>> GetLikeZodiacById(int id)
        {
            var response = new ServiceResponse<LikeZodiacDTO?>();
            try
            {
                var likeZodiac = await _repository.GetLikeZodiacByIdAsync(id);
                if (likeZodiac == null)
                {
                    response.Success = false;
                    response.Message = "LikeZodiac not found";
                    return response;
                }
                response.Data = _mapper.Map<LikeZodiacDTO>(likeZodiac);
                response.Success = true;
                response.Message = "Get LikeZodiac by id successfully";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<string>> UpdateLikeZodiacAsync(int id, CreateLikeZodiacDTO updateDto)
        {
            var response = new ServiceResponse<string>();
            try
            {
                var likeZodiac = await _repository.GetByIdAsync(id);
                if (likeZodiac == null)
                {
                    response.Success = false;
                    response.Message = "LikeZodiac not found";
                    return response;
                }
                likeZodiac = _mapper.Map<LikeZodiac>(updateDto);
                await _repository.Update(likeZodiac);
                response.Success = true;
                response.Message = "LikeZodiac updated successfully";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
