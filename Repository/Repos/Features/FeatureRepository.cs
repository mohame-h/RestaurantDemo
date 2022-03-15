using Dapper;
using Domain.Entities;
using Repository.QueriesConnectionFactory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Repository.Features
{
    public class FeatureRepository : IFeatureRepository
    {
        private DBEntities _dbCommands;
        private IQueryConnection _dbQueriesConnection;
        private IDbConnection _dbQueries;

        public FeatureRepository(DBEntities dbCommands, IQueryConnection dbQueriesConnection)
        {
            _dbCommands = dbCommands;
            _dbQueriesConnection = dbQueriesConnection;
            _dbQueries = _dbQueriesConnection.OpenConnection();
        }

        public bool Add(Feature feature)
        {
            try
            {
                feature.CreatedAt = DateTime.Now;
                feature.IsActive = true;
                feature.IsDeleted = false;

                _dbCommands.Features.Add(feature);
                _dbCommands.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Feature> GetAll(PaginationData pagination)
        {
            var getAllFeaturesSQL = @"SELECT Name, Description
                                    FROM Feature
                                    WHERE IsDeleted= 0 AND IsActive= 1
                                    OFFSET @Skip Rows
                                    FETCH NEXT @Take Rows ONLY";
            var users = _dbQueries.Query<Feature>(getAllFeaturesSQL, new { Skip = ((pagination.PageNumber - 1) * pagination.RequiredItemsCount), Take = pagination.RequiredItemsCount });
            return users;
        }

        public int GetAllCount()
        {
            var totalFeaturesSQL = @"SELECT COUNT(Id) 
                                    FROM Feature
                                    WHERE IsDeleted= 0 AND IsActive= 1";
            var totalFeatures = _dbQueries.ExecuteScalar<int>(totalFeaturesSQL);
            return totalFeatures;
        }

        public Feature GetBy(int id)
        {
            var getFeatureSQL = @"SELECT Name, Description
                                    FROM Feature
                                    WHERE Id = @id AND IsDeleted= 0 AND IsActive= 1";
            var result = _dbQueries.QueryFirstOrDefault<Feature>(getFeatureSQL, new { id });
            return result;
        }

        public Feature GetBy(string name)
        {
            var getFeatureSQL = @"SELECT Name, Description
                                    FROM Feature
                                    WHERE Name = @name AND IsDeleted= 0 AND IsActive= 1";
            var result = _dbQueries.QueryFirstOrDefault<Feature>(getFeatureSQL, new { name });
            return result;
        }
        public IEnumerable<Feature> GetByDefaultAssociatedRole(int role)
        {
            var getFeatureSQL = @"SELECT Name, Description
                                    FROM Feature
                                    WHERE Id = @role AND IsDeleted= 0 AND IsActive= 1";
            var result = _dbQueries.Query<Feature>(getFeatureSQL, new { role });
            return result;
        }
        public IEnumerable<int> GetIdsByDefaultAssociatedRole(int role)
        {
            var getFeatureSQL = @"SELECT Id
                                    FROM Feature
                                    WHERE Id = @role AND IsDeleted= 0 AND IsActive= 1";
            var result = _dbQueries.Query<int>(getFeatureSQL, new { role });
            return result;
        }
        public bool Update(Feature feature)
        {
            var oldFeature = _dbCommands.Features.FirstOrDefault(z => z.Id == feature.Id && z.IsActive == true && z.IsDeleted == false);
            if (oldFeature == null)
                return false;

            oldFeature.Name = feature.Name;
            oldFeature.Description = feature.Description;

            oldFeature.UpdatedAt = DateTime.Now;

            try
            {
                _dbCommands.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveBy(int id)
        {
            var feature = _dbCommands.Features.FirstOrDefault(z => z.Id == id && z.IsActive == true && z.IsDeleted == false);
            if (feature == null)
                return false;

            feature.IsActive = false;
            feature.IsDeleted = true;
            feature.DeletedAt = DateTime.Now;
            try
            {
                _dbCommands.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveBy(string name)
        {
            var feature = _dbCommands.Features.FirstOrDefault(z => z.Name == name && z.IsActive == true && z.IsDeleted == false);
            if (feature == null)
                return false;

            feature.IsActive = false;
            feature.IsDeleted = true;
            feature.DeletedAt = DateTime.Now;
            try
            {
                _dbCommands.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }




    }
}
