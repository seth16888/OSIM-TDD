﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NBehave.Spec.NUnit;
using NHibernate;
using NUnit.Framework;
using OSIM.Core.Entities;
using OSIM.Core.Persistence;
using Rhino.Mocks;

namespace OSIM.UnitTest.OSIM.Core
{
    public class when_working_with_the_item_type_reository : Specification
    {

    }

    public class and_saving_a_valid_item_type : when_working_with_the_item_type_reository
    {
        private int _result;
        private IItemTypeRepository _itemTypeRepository;
        private ItemType _testItemType;
        private int _itemTypeId;

        protected override void Establish_context()
        {
            base.Establish_context();

            var randomNumberGenerator = new Random();
            _itemTypeId = randomNumberGenerator.Next(3200);

            var sessionFactory = new Mock<ISessionFactory>();
            var session = new Mock<ISession>();
            session.Setup(s => s.Save(_testItemType)).Returns(_itemTypeId);

            sessionFactory.Setup(sf => sf.OpenSession()).Returns(session.Object);

            _itemTypeRepository = new ItemTypeRepository(sessionFactory.Object);
        }

        protected override void Because_of()
        {
            
            _result = _itemTypeRepository.Save(_testItemType);
        }

        [Test]
        public void then_a_valid_item_type_id_should_be_returned()
        {
           _result.ShouldEqual(_itemTypeId);
        }
    }
}
