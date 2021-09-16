using System.Collections.Generic;

namespace CatFactHerokuWebClientApp.Web.Domain
{
    public interface IMapper<T, TDto>
    {
        /// <summary>
        /// create a new Entity object and map DTO model to it 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        T ToEntity(TDto dto);
        /// <summary>
        /// map dto model to existing entity object
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="entity"></param>
        void ToEntity(TDto dto, T entity);
        /// <summary>
        /// create new dto object and map Entity to it
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TDto ToDto(T entity);
        /// <summary>
        /// create a list of new DTOs and map the entities to it
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        IEnumerable<TDto> ToDto(IEnumerable<T> entities);
        /// <summary>
        /// create a list of new entities and map the dtoes to it
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        IEnumerable<T> ToEntity(IEnumerable<TDto> dtos);
    }
}
