using Application.Abstract;
using Application.Dto;
using AutoMapper;
using Core.Models;
using Persistence.Abstract;
using Persistence.Models;

namespace Application.Services;

public class SchoolService : ISchoolService
{
    public SchoolService(ITeacherRepository teacherRepository, IStudentRepository studentRepository, 
        ISchoolRepository schoolRepository, IUserRepository userRepository, IPostRepository postRepository,
        IMapper mapper)
    {
        _userRepository = userRepository;
        
        _teacherRepository = teacherRepository;
        _studentRepository = studentRepository;
        
        _schoolRepository = schoolRepository;

        _postRepository = postRepository;
        
        
        _mapper = mapper;
    }

    private readonly IPostRepository _postRepository;
    
    private readonly IUserRepository _userRepository;
    private readonly ISchoolRepository _schoolRepository;
    
    private readonly ITeacherRepository _teacherRepository;
    private readonly IStudentRepository _studentRepository;
    
    private readonly IMapper _mapper;

    public async Task CreateSchoolAsync(CreateSchoolRequestDto request)
    {
        await CheckTeacherPermissionsAsync(request.AuthorId);

        Teacher teacher = await _teacherRepository.GetByUserIdAsync(request.AuthorId)
                          ?? throw new Exception("No such students with that userId");
        if (teacher.School != null)
        {
            throw new Exception("This teacher already has school");
        }
        
        School school = GenerateSchoolObjectOnCreatingSchool(request, teacher);

        await _schoolRepository.AddSchoolAsync(school);
    }

    public async Task JoinSchoolAsync(JoinSchoolRequestDto request)
    {
        await CheckStudentPermissionsAsync(request.UserId);
        
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
        await CheckSchoolExistenceByUserIdAsync(userId);

        return await GetSchoolByUserIdAsyncHelper(userId);
    }
    
    public async Task LeaveSchool(int userId)
    {
        await CheckStudentPermissionsAsync(userId);

        int? studentId = await _studentRepository.GetStudentIdByUserIdAsync(userId);
        if (studentId == null)
        {
            return;
        }

        int studentIdValue = studentId.Value;
        
        await CheckSchoolExistenceByUserIdAsync(userId);

        await _schoolRepository.LeaveSchoolAsync(studentIdValue);
    }

    public async Task UpdateSchool(UpdateSchoolRequestDto request)
    {
        await CheckTeacherPermissionsAsync(request.UserId);
        
        await CheckSchoolExistenceByUserIdAsync(request.UserId);

        int schoolId = await GetSchoolIdByUserIdForTeacherAsync(request.UserId);
        SchoolUpdatingModel model = _mapper.Map<UpdateSchoolRequestDto, SchoolUpdatingModel>(request);
        
        await _schoolRepository.UpdateAsync(model, schoolId);
    }
    
    public async Task DeleteSchool(DeleteSchoolRequestDto request)
    {
        await CheckTeacherPermissionsAsync(request.UserId);

        await CheckSchoolExistenceByUserIdAsync(request.UserId);

        int schoolId = await GetSchoolIdByUserIdForTeacherAsync(request.UserId);

        await _schoolRepository.DeleteByIdAsync(schoolId);
    }

    public async Task CreatePost(CreatePostRequestDto request)
    {
        await CheckTeacherPermissionsAsync(request.UserId);
        
        int schoolId = await GetSchoolIdByUserIdForTeacherAsync(request.UserId);

        Post post = Post.Create(request.Data);

        await _postRepository.AddAsync(schoolId, post);
    }

    public async Task UpdatePost(EditPostRequestDto request)
    {
        await CheckTeacherPermissionsAsync(request.UserId);

        await _postRepository.UpdateAsync(request.Data, request.PostId);
    }

    public async Task DeletePost(DeletePostRequestDto request)
    {
        await CheckTeacherPermissionsAsync(request.UserId);

        await _postRepository.DeleteAsync(request.PostId);
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
    
    private async Task<int> GetSchoolIdByUserIdForTeacherAsync(int userId)
    {
        await CheckSchoolExistenceByUserIdAsync(userId);

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


    private async Task CheckSchoolExistenceByUserIdAsync(int userId)
    {
        User user = await _userRepository.GetByIdAsync(userId)
                    ?? throw new Exception("No such users with that id");
        
        if (user.Teacher != null)
        {
            CheckSchoolExistenceForTeacher(user.Teacher);
            return;
        }

        if (user.Student != null)
        {
            CheckSchoolExistenceForStudent(user.Student);
            return;
        }

        throw new Exception("That user is not student nor teacher");
    }

    private void CheckSchoolExistenceForTeacher(Teacher teacher, string exceptionMessage = "That teacher does not have a school")
    {
        if (teacher.School == null)
        {
            throw new Exception(exceptionMessage);
        }
    }
    
    private void CheckSchoolExistenceForStudent(Student student, string exceptionMessage = "That student does not have a school")
    {
        if (student.School == null)
        {
            throw new Exception(exceptionMessage);
        }
    }
    
    private async Task CheckStudentPermissionsAsync(int userId, string exceptionMessage = "You have no student permissions to do it")
    {
        bool isStudent = await _studentRepository.IsStudentByUserIdAsync(userId);
        if (!isStudent)
        {
            throw new Exception(exceptionMessage);
        }
    }
    private async Task CheckTeacherPermissionsAsync(int authorId, string exceptionMessage = "You have no teacher permissions to do it")
    {
        bool isTeacher = await _teacherRepository.IsTeacherByUserIdAsync(authorId);
        if (!isTeacher)
        {
            throw new Exception(exceptionMessage);
        }
    }
    
    private School GenerateSchoolObjectOnCreatingSchool(CreateSchoolRequestDto request, Teacher teacher)
    {
        return School.CreateSchool(
            request.CourseName,
            request.Description,
            teacher,
            request.Subject);
    }
}