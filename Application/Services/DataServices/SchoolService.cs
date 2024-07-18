using Application.Abstract;
using Application.Dto.Post;
using Application.Dto.School;
using AutoMapper;
using Core.Models;
using Core.StaticInfoModels;
using Persistence.Abstract;
using Persistence.Models;

namespace Application.Services.DataServices;

public class SchoolService : ISchoolService
{
    public SchoolService(ITeacherRepository teacherRepository, IStudentRepository studentRepository, 
        ISchoolRepository schoolRepository, IUserRepository userRepository, IPostRepository postRepository,
        ISchoolPermissionsHelper schoolPermissionsHelper, IMapper mapper, ISubjectRepository subjectRepository)
    {
        _userRepository = userRepository;
        _teacherRepository = teacherRepository;
        
        _studentRepository = studentRepository;
        _subjectRepository = subjectRepository;
        
        _schoolRepository = schoolRepository;
        _postRepository = postRepository;
        
        _permissionsHelper = schoolPermissionsHelper;
        
        _mapper = mapper;

    }

    private readonly IPostRepository _postRepository;
    
    private readonly IUserRepository _userRepository;
    private readonly ISchoolRepository _schoolRepository;
    
    private readonly ITeacherRepository _teacherRepository;
    private readonly IStudentRepository _studentRepository;

    private readonly ISchoolPermissionsHelper _permissionsHelper;

    private readonly ISubjectRepository _subjectRepository;
    
    private readonly IMapper _mapper;

    public async Task CreateSchoolAsync(CreateSchoolDto request)
    {
        await _permissionsHelper.CheckTeacherPermissionsAsync(request.AuthorId);

        Teacher teacher = await _teacherRepository.GetByUserIdAsync(request.AuthorId)
                          ?? throw new Exception("No such students with that userId");
        if (teacher.School != null)
        {
            throw new Exception("This teacher already has school");
        }

        Subject subject = await _subjectRepository.GetByIdAsync(request.SubjectId)
                          ?? throw new Exception("No such a subject with this name");


        School school = School.CreateSchool(request.CourseName, request.Description, teacher, subject);

        await _schoolRepository.AddSchoolAsync(school);
    }

    public async Task JoinSchoolAsync(JoinSchoolDto request)
    {
        await _permissionsHelper.CheckStudentPermissionsAsync(request.UserId);
        
        Student student = await _studentRepository.GetByUserIdAsync(request.UserId)
                          ?? throw new Exception();
        
        int studentIdValue = student.Id!.Value;
        bool containsStudent = await _schoolRepository.ContainsStudentByIdAsync(studentId: studentIdValue,
            schoolId: request.SchoolId);
        if (containsStudent)
        {
            throw new Exception("School already contains that student");
        }

        await _schoolRepository.AddStudentToSchoolAsync(student, request.SchoolId);
    }

    public async Task<School?> GetByUserIdAsync(int userId)
    {
        await _permissionsHelper.CheckSchoolExistenceByUserIdAsync(userId);

        return await GetSchoolByUserIdAsyncHelper(userId);
    }
    
    public async Task LeaveSchool(int userId)
    {
        await _permissionsHelper.CheckStudentPermissionsAsync(userId);

        int? studentId = await _studentRepository.GetStudentIdByUserIdAsync(userId);
        if (studentId == null)
        {
            return;
        }

        int studentIdValue = studentId.Value;
        
        await _permissionsHelper.CheckSchoolExistenceByUserIdAsync(userId);

        await _schoolRepository.LeaveSchoolAsync(studentIdValue);
    }

    public async Task UpdateSchool(UpdateSchoolDto request)
    {
        await _permissionsHelper.CheckTeacherPermissionsAsync(request.UserId);
        
        await _permissionsHelper.CheckSchoolExistenceByUserIdAsync(request.UserId);

        int schoolId = await GetSchoolIdByUserIdForTeacherAsync(request.UserId);
        SchoolUpdatingModel model = _mapper.Map<UpdateSchoolDto, SchoolUpdatingModel>(request);
        
        await _schoolRepository.UpdateAsync(model, schoolId);
    }
    
    public async Task DeleteSchool(DeleteSchoolDto request)
    {
        await _permissionsHelper.CheckTeacherPermissionsAsync(request.UserId);

        await _permissionsHelper.CheckSchoolExistenceByUserIdAsync(request.UserId);

        int schoolId = await GetSchoolIdByUserIdForTeacherAsync(request.UserId);

        await _schoolRepository.DeleteByIdAsync(schoolId);
    }

    public async Task CreatePost(CreatePostDto request)
    {
        await _permissionsHelper.CheckTeacherPermissionsAsync(request.UserId);
        
        int schoolId = await GetSchoolIdByUserIdForTeacherAsync(request.UserId);

        Post post = Post.Create(request.Data);

        await _postRepository.AddAsync(schoolId, post);
    }

    public async Task UpdatePost(EditPostDto request)
    {
        await _permissionsHelper.CheckTeacherPermissionsAsync(request.UserId);

        await _postRepository.UpdateAsync(request.Data, request.ObjectToUpdateId);
    }

    public async Task DeletePost(DeletePostDto request)
    {
        await _permissionsHelper.CheckTeacherPermissionsAsync(request.UserId);

        await _postRepository.DeleteAsync(request.PostId);
    }
    
    private async Task<int> GetSchoolIdByUserIdForTeacherAsync(int userId)
    {
        await _permissionsHelper.CheckSchoolExistenceByUserIdAsync(userId);

        int? teacherId = await _teacherRepository.GetTeacherIdByUserIdAsync(userId);
        if (teacherId == null)
        {
            throw new Exception("This user is not teacher");
        }
        int teacherIdValue = teacherId.Value;
        
        
        School school = await _schoolRepository.GetSchoolByTeacherIdAsync(teacherIdValue)
                        ?? throw new Exception("That teacher does not have a school");
        return school.Id!.Value;
    }
    
    private async Task<School?> GetSchoolByUserIdAsyncHelper(int userId)
    {
        User user = await _userRepository.GetByIdAsync(userId)
                    ?? throw new Exception("No such users with that id");
        
        if (user.Teacher != null)
        {
            int teacherId = user.Teacher.Id!.Value;
            
            return await _schoolRepository.GetSchoolByTeacherIdAsync(teacherId);
        }

        if (user.Student != null)
        {
            int studentId = user.Student.Id!.Value;

            return await _schoolRepository.GetSchoolByStudentIdAsync(studentId);
        }

        throw new Exception("This user does not have a school");
    }
}