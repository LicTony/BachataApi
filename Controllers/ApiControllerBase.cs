using BachataApi.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BachataApi.Controllers
{
    public class ApiControllerBase : ControllerBase
    {
        #region Success Responses

        // Respuesta 200 OK con datos
        protected IActionResult OkResponse(object data) =>
            Ok(data);

        // Respuesta 201 Created
        protected IActionResult CreatedResponse(string uri, object data) =>
            Created(uri, data);

        // Respuesta 201 Created usando CreatedAtAction
        protected IActionResult CreatedAtActionResponse(string actionName, object routeValues, object value)
        {
            return CreatedAtAction(actionName, routeValues, value);
        }

        // Respuesta 202 Accepted
        protected IActionResult AcceptedResponse(string message) =>
            Accepted(new { message });

        // Respuesta 204 No Content
        protected IActionResult NoContentResponse() =>
            NoContent();

        #endregion

        #region BadRequest Responses

        // Respuesta de error 400 con mensaje y detalles
        protected IActionResult BadRequestResponse(string message, object? details = null) =>
            BadRequest(new ErrorResponse
            {
                StatusCode = 400,
                Message = message,
                Details = details
            });

        // Respuesta de error 400 genérica
        protected IActionResult BadRequestResponse(object? details = null) =>
            BadRequest(new ErrorResponse
            {
                StatusCode = 400,
                Message = "Bad Request",
                Details = details
            });

        #endregion

        #region Unauthorized Responses

        // Respuesta de error 401 con mensaje y detalles
        protected IActionResult UnauthorizedResponse(string message, object? details = null) =>
            Unauthorized(new ErrorResponse
            {
                StatusCode = 401,
                Message = message,
                Details = details
            });

        // Respuesta de error 401 genérica
        protected IActionResult UnauthorizedResponse(object? details = null) =>
            Unauthorized(new ErrorResponse
            {
                StatusCode = 401,
                Message = "Unauthorized",
                Details = details
            });

        #endregion

        #region NotFound Responses

        // Respuesta de error 404 con mensaje y detalles
        protected IActionResult NotFoundResponse(string message, object? details = null) =>
            NotFound(new ErrorResponse
            {
                StatusCode = 404,
                Message = message,
                Details = details
            });

        // Respuesta de error 404 genérica
        protected IActionResult NotFoundResponse(string? message = null) =>
            NotFound(new ErrorResponse
            {
                StatusCode = 404,
                Message = "Recurso no encontrado",
                Details = message ?? "El recurso solicitado no existe."
            });

        #endregion

        #region Conflict Responses
        
        // Respuesta de error 409 (conflicto)
        protected IActionResult ConflictResponse(object? details = null) =>
            Conflict(new ErrorResponse
            {
                StatusCode = 409,
                Message = "Conflicto",
                Details = details
            });

        #endregion

        #region Unprocessable Entity Responses

        // Respuesta de error 422 (entidad no procesable)
        protected IActionResult UnprocessableEntityResponse(object? details = null) =>
            UnprocessableEntity(new ErrorResponse
            {
                StatusCode = 422,
                Message = "Unprocessable Entity",
                Details = details
            });

        #endregion
    }

}
