using System;
using FluentMigrator.Expressions;
using FluentMigrator.Infrastructure;
using FluentMigrator.Tests.Helpers;
using Xunit;

namespace FluentMigrator.Tests.Unit.Expressions
{
	public class RenameTableExpressionTests
	{
		[Fact]
		public void ErrorIsReturnedWhenOldNameIsNull()
		{
			var expression = new RenameTableExpression { OldName = null };
			var errors = ValidationHelper.CollectErrors(expression);
			Assert.Contains(ErrorMessages.OldTableNameCannotBeNullOrEmpty, errors);
		}

		[Fact]
		public void ErrorIsReturnedWhenOldNameIsEmptyString()
		{
			var expression = new RenameTableExpression { OldName = String.Empty };
			var errors = ValidationHelper.CollectErrors(expression);
			Assert.Contains(ErrorMessages.OldTableNameCannotBeNullOrEmpty, errors);
		}

		[Fact]
		public void ErrorIsNotReturnedWhenOldNameIsNotNullEmptyString()
		{
			var expression = new RenameTableExpression { OldName = "Bacon" };
			var errors = ValidationHelper.CollectErrors(expression);
			Assert.DoesNotContain(ErrorMessages.OldTableNameCannotBeNullOrEmpty, errors);
		}

		[Fact]
		public void ErrorIsReturnedWhenNewNameIsNull()
		{
			var expression = new RenameTableExpression { NewName = null };
			var errors = ValidationHelper.CollectErrors(expression);
			Assert.Contains(ErrorMessages.NewTableNameCannotBeNullOrEmpty, errors);
		}

		[Fact]
		public void ErrorIsReturnedWhenNewNameIsEmptyString()
		{
			var expression = new RenameTableExpression { NewName = String.Empty };
			var errors = ValidationHelper.CollectErrors(expression);
			Assert.Contains(ErrorMessages.NewTableNameCannotBeNullOrEmpty, errors);
		}

		[Fact]
		public void ErrorIsNotReturnedWhenNewNameIsNotNullOrEmptyString()
		{
			var expression = new RenameTableExpression { NewName = "Bacon" };
			var errors = ValidationHelper.CollectErrors(expression);
			Assert.DoesNotContain(ErrorMessages.NewTableNameCannotBeNullOrEmpty, errors);
		}

		[Fact]
		public void ReverseReturnsRenameTableExpression()
		{
			var expression = new RenameTableExpression { OldName = "Bacon", NewName = "ChunkyBacon" };
			var reverse = expression.Reverse();
			Assert.IsType<RenameTableExpression>(reverse);
		}

		[Fact]
		public void ReverseSetsOldNameAndNewNameOnGeneratedExpression()
		{
			var expression = new RenameTableExpression { OldName = "Bacon", NewName = "ChunkyBacon" };
			var reverse = expression.Reverse() as RenameTableExpression;
			Assert.Equal("ChunkyBacon", reverse.OldName);
			Assert.Equal("Bacon", reverse.NewName);
		}
	}
}