using System;
using System.Collections;
using System.Collections.Generic;
using MyGameServer.Model;
using NHibernate;
using NHibernate.Criterion;

namespace MyGameServer.Managers
{
    class UserManager : IUserManager
    {
        public void Add(User user)
        {
            // 用法1
            //ISession session = NHibernateHelper.OpenSession();
            //session.Save(user);
            //session.Close();

            // 用法2
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction()) // 使用事务
                {
                    session.Save(user);
                    transaction.Commit();
                }
            }
        }

        public void Update(User user)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction()) // 使用事务
                {
                    session.Update(user);// user 必须包含有主键，在 nhibernate 中是 id
                    transaction.Commit();
                }
            }
        }

        public void Remove(User user)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction()) // 使用事务
                {
                    session.Delete(user);
                    transaction.Commit();
                }
            }
        }

        public User GetById(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction()) // 使用事务
                {
                    var user = session.Get<User>(id);
                    transaction.Commit();
                    return user;
                }
            }
        }

        public User GetByUsername(string username)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                //ICriteria criteria = session.CreateCriteria(typeof(User));
                //criteria.Add(Restrictions.Eq("Username", username));
                //User user = criteria.UniqueResult<User>();
                User user = session.CreateCriteria<User>().Add(Restrictions.Eq("Username", username)).UniqueResult<User>();
                return user;
            }
        }

        public ICollection<User> GetAllUsers()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                IList<User> users = session.CreateCriteria(typeof(User)).List<User>();
                return users;
            }
        }

        public bool VerifyUser(string username, string password)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                User user = session.CreateCriteria(typeof(User))
                    .Add(Restrictions.Eq("Username", username))
                    .Add(Restrictions.Eq("Password", password))
                    .UniqueResult<User>();

                return user != null;
            }
        }
    }
}
