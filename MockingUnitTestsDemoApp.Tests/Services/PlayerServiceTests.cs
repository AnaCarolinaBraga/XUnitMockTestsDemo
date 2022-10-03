using MockingUnitTestsDemoApp.Impl.Models;
using MockingUnitTestsDemoApp.Impl.Repositories.Interfaces;
using MockingUnitTestsDemoApp.Impl.Services;
using MockingUnitTestsDemoApp.Impl.Services.Interfaces;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MockingUnitTestsDemoApp.Tests.Services
{
    public class PlayerServiceTests
    {
        private readonly PlayerService _subject;
        private readonly IPlayerRepository _mockIPlayerRepository;
        private readonly ILeagueRepository _mockILeagueRepository;
        private readonly ITeamRepository _mockITeamRepository;
        public PlayerServiceTests()
        {
            _mockIPlayerRepository = Substitute.For<IPlayerRepository>();
            _mockITeamRepository = Substitute.For<ITeamRepository>();
            _mockILeagueRepository = Substitute.For<ILeagueRepository>();

            _subject = new PlayerService(_mockIPlayerRepository, _mockITeamRepository, _mockILeagueRepository);
        }

        [Fact]
        public void GetForLeague_HappyDay_RetornaPlayers()
        {
            //arrange
            _mockILeagueRepository.IsValid(1).Returns(true);
            _mockITeamRepository.GetForLeague(1).Returns(GetFakeTeams().Where(x => x.LeagueID == 1).ToList());
            _mockIPlayerRepository.GetForTeam(5).Returns(GetFakePlayers());
            var expectedResult = GetFakePlayers().Where(x => x.TeamID == 5).ToList();

            //act
            var result = _subject.GetForLeague(1);

            //assert
            Assert.Equal(expectedResult.ToString(), result.ToString());
        }

        [Fact]
        public void GetForLeague_LigaInexistente_RetornaLista()
        {
            //arrange
            _mockILeagueRepository.IsValid(10).Returns(false);
            var expectedResult = new List<Player>();

            //act
            var result = _subject.GetForLeague(10);

            //assert
            Assert.Equal(expectedResult.ToString(), result.ToString());
        }

        [Fact]
        public void GetForLeague_LigaSemTime_RetornaNovaLista()
        {
            //arrange
            _mockILeagueRepository.IsValid(4).Returns(true);
            _mockITeamRepository.GetForLeague(4).Returns(GetFakeTeams().Where(x => x.LeagueID == 4).ToList());
            _mockIPlayerRepository.GetForTeam(Arg.Any<int>()).Returns(GetFakePlayers());
            var expectedResult = new List<Player>();

            //act
            var result = _subject.GetForLeague(1);


            //assert
            Assert.Equal(expectedResult.ToString(), result.ToString());
        }

        private List<Player> GetFakePlayers()
        {
            return new List<Player>
            {
                new Player { ID = 1, FirstName = "Ana", LastName = "Braga", DateOfBirth = DateTime.Parse("5/10/1999") , TeamID = 1},
                new Player { ID = 2, FirstName = "Alice", LastName = "Kelson", DateOfBirth = DateTime.Parse("1/10/2000") , TeamID = 3},
                new Player { ID = 3, FirstName = "Cecilia", LastName = "Martins", DateOfBirth = DateTime.Parse("1/1/2001") , TeamID = 5},
                new Player { ID = 4, FirstName = "Barbara", LastName = "Almeida", DateOfBirth = DateTime.Parse("12/2/1995") , TeamID = 4},
                new Player { ID = 5, FirstName = "Fabiana", LastName = "dos Santos", DateOfBirth = DateTime.Parse("9/9/1994") , TeamID = 3},
                new Player { ID = 6, FirstName = "Leticia", LastName = "Mangueira", DateOfBirth = DateTime.Parse("11/11/1993") , TeamID = 1},
                new Player { ID = 7, FirstName = "Paula", LastName = "Giovanni", DateOfBirth = DateTime.Parse("12/12/1992") , TeamID = 4},
                new Player { ID = 8, FirstName = "Viviane", LastName = "Collor", DateOfBirth = DateTime.Parse("10/6/2000") , TeamID = 3},
                new Player { ID = 9, FirstName = "Fatima", LastName = "Inacio", DateOfBirth = DateTime.Parse("2/9/2000") , TeamID = 3},
                new Player { ID = 10, FirstName = "Joice", LastName = "Silva", DateOfBirth = DateTime.Parse("6/10/2001") , TeamID = 4},
                new Player { ID = 11, FirstName = "Lilian", LastName = "Pereira", DateOfBirth = DateTime.Parse("5/5/2003") , TeamID = 2},
            };
        }

        private List<League> GetFakeLeagues()
        {
            return new List<League>()
            {
            new League { ID = 1, Name = "A", FoundingDate = DateTime.Parse("12/12/1900") },
            new League { ID = 2, Name = "B", FoundingDate = DateTime.Parse("1/1/1800") },
            new League { ID = 3, Name = "C", FoundingDate = DateTime.Parse("3/3/1950") },
            new League { ID = 4, Name = "D", FoundingDate = DateTime.Parse("7/7/2000") }
            };
        }

        private List<Team> GetFakeTeams()
        {
            return new List<Team>()
            {
                new Team { ID =1 ,Name = "Paladinas", FoundingDate = DateTime.Parse("1/10/1970"), LeagueID =3 },
                new Team { ID =2 ,Name = "Clerigas", FoundingDate = DateTime.Parse("1/10/1980"), LeagueID =2 },
                new Team { ID =3 ,Name = "Magas", FoundingDate = DateTime.Parse("1/10/1990"), LeagueID =3 },
                new Team { ID =4 ,Name = "Guerreiras", FoundingDate = DateTime.Parse("1/10/2000"), LeagueID =2 },
                new Team { ID =5 ,Name = "Druidas", FoundingDate = DateTime.Parse("1/10/2010"), LeagueID =1 },
                new Team { ID =5 ,Name = "Artifices", FoundingDate = DateTime.Parse("13/3/2005"), LeagueID =3 },
            };
        }
    }
}
