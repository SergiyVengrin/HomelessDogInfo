using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AddedRatingColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "articleid_constraint",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "ArticleID",
                table: "Comments",
                newName: "Rating");

            migrationBuilder.AddColumn<int>(
                name: "DogID",
                table: "Comments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddCheckConstraint(
                name: "dogid_constraint",
                table: "Comments",
                sql: "\"DogID\" >=0");

            migrationBuilder.AddCheckConstraint(
                name: "ratingconstraint",
                table: "Comments",
                sql: "\"Rating\" >= 1 AND \"Rating\" <= 5");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "dogid_constraint",
                table: "Comments");

            migrationBuilder.DropCheckConstraint(
                name: "ratingconstraint",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "DogID",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "Rating",
                table: "Comments",
                newName: "ArticleID");

            migrationBuilder.AddCheckConstraint(
                name: "articleid_constraint",
                table: "Comments",
                sql: "\"ArticleID\" >=0");
        }
    }
}
