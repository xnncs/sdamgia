namespace Application.Abstract;

public interface ISchoolPermissionsHelper
{
    public Task CheckSchoolExistenceByUserIdAsync(int userId);

    public Task CheckStudentPermissionsAsync(int userId,
        string exceptionMessage = "You have no student permissions to do it");

    public Task CheckTeacherPermissionsAsync(int userId,
        string exceptionMessage = "You have no teacher permissions to do it");
}