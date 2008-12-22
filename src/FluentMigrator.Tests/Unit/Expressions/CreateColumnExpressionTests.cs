using System;
using FluentMigrator.Expressions;
using FluentMigrator.Infrastructure;
using FluentMigrator.Tests.Helpers;
using Xunit;

namespace FluentMigrator.Tests.Unit.Expressions
{
	public class CreateColumnExpressionTests
	{
		[Fact]
		public void ErrorIsReturnedWhenOldNameIsNull()
		{
			var expression = new CreateColumnExpression { TableName = null };
			var errors = ValidationHelper.CollectErrors(expression);
			Assert.Contains(ErrorMessages.TableNameCannotBeNullOrEmpty, errors);
		}

		[Fact]
		public void ErrorIsReturnedWhenOldNameIsEmptyString()
		{
			var expression = new CreateColumnExpression { TableName = String.Empty };
			var errors = ValidationHelper.CollectErrors(expression);
			Assert.Contains(ErrorMessages.TableNameCannotBeNullOrEmpty, errors);
		}

		[Fact]
		public void ErrorIsNotReturnedWhenOldNameIsNotNullEmptyString()
		{
			var expression = new CreateColumnExpression { TableName = "Bacon" };
			var errors = ValidationHelper.CollectErrors(expression);
			Assert.DoesNotContain(ErrorMessages.TableNameCannotBeNullOrEmpty, errors);
		}

		[Fact]
		public void ReverseReturnsDeleteColumnExpression()
		{
			var expression = new CreateColumnExpression { TableName = "Bacon", Column = { Name = "BaconId" } };
			var reverse = expression.Reverse();
			Assert.IsType<DeleteColumnExpression>(reverse);
		}

		[Fact]
		public void ReverseSetsTableNameAndColumnNameOnGeneratedExpression()
		{
			var expression = new CreateColumnExpression { TableName = "Bacon", Column = { Name = "BaconId" } };
			var reverse = expression.Reverse() as DeleteColumnExpression;
			Assert.Equal(reverse.TableName, "Bacon");
			Assert.Equal(reverse.ColumnName, "BaconId");
		}
	}
}