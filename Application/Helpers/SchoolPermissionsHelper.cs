using Application.Abstract;
using Core.Models;
using Persistence.Abstract;

namespace Application.Helpers;

public class SchoolPermissionsHelper : ISchoolPermissionsHelper
{
    public SchoolPermissionsHelper(IPostRepository postRepository, IUserRepository userRepository,
        ISchoolRepository schoolRepository,
        ITeacherRepository teacherRepository, IStudentRepository studentRepository)
    {
        _postRepository = postRepository;

        _userRepository = userRepository;
        _schoolRepository = schoolRepository;

        _teacherRepository = teacherRepository;
        _studentRepository = studentRepository;
    }
    
    private readonly IPostRepository _postRepository;
    
    private readonly IUserRepository _userRepository;
    private readonly ISchoolRepository _schoolRepository;
    
    private readonly ITeacherRepository _teacherRepository;
    private readonly IStudentRepository _studentRepository;
    

    public async Task CheckSchoolExistenceByUserIdAsync(int userId)
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
    
    public async Task CheckStudentPermissionsAsync(int userId, string exceptionMessage = "You have no student permissions to do it")
    {
        bool isStudent = await _studentRepository.IsStudentByUserIdAsync(userId);
        if (!isStudent)
        {
            throw new Exception(exceptionMessage);
        }
    }
    public async Task CheckTeacherPermissionsAsync(int userId, string exceptionMessage = "You have no teacher permissions to do it")
    {
        bool isTeacher = await _teacherRepository.IsTeacherByUserIdAsync(userId);
        if (!isTeacher)
        {
            throw new Exception(exceptionMessage);
        }
    }
}