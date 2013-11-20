using System.Collections.Generic;
using System.Linq;
using System.Windows;
using FootballSchool.Models;
using FootballSchool.Repositories;

namespace FootballSchool.Pages.Main
{
	/// <summary>
	/// enum for game result
	/// </summary>
	public enum GameResult : byte
	{
		Win, Draw, Lose
	}

	/// <summary>
	/// Interaction logic for TableStats.xaml
	/// </summary>
	public partial class TableStats
	{
		private TeamRepository _teamRepository;
		private GameRepository _gameRepository;
		private GameEventRepository _gameEventRepository;

		public TableStats()
		{
			InitializeComponent();
			_teamRepository = new TeamRepository();
			_gameRepository = new GameRepository();
			_gameEventRepository = new GameEventRepository();
		}


		private List<TableStatModel> GetTableStats()
		{
			var result = new List<TableStatModel>();

			var teams = _teamRepository.GetAll();
			foreach (var team in teams)
			{
				var row = new TableStatModel { Команда = team.Name };

				var gamesForTeam = _gameRepository.GetGamesForTeam(team.Id);

				foreach (var game in gamesForTeam)
				{
					ComputeGame(game, row);
				}

				row.Очков = ComputeScore(row);
				result.Add(row);
			}
			result = result.OrderByDescending(x => x.Очков).ToList();

			foreach (var tableStatModel in result)
			{
				tableStatModel.Место = result.IndexOf(tableStatModel);
			}

			return result;
		}

		private void ComputeGame(Games game, TableStatModel row)
		{
			var gameEvents = _gameEventRepository.GetEventsForGame(game.Id);
			var gameResult = ComputeGameResult(gameEvents, row);
			SetGameResult(row, gameResult);
		}

		private static void SetGameResult(TableStatModel row, GameResult gameResult)
		{
			switch (gameResult)
			{
				case GameResult.Win:
					{
						row.Победы++;
						break;
					}
				case GameResult.Draw:
					{
						row.Ничьи++;
						break;
					}
				default:
					{
						row.Поражения++;
						break;
					}
			}
		}

		/// <summary>
		/// Computes the score.
		/// </summary>
		/// <param name="row">The row.</param>
		/// <returns></returns>
		private int ComputeScore(TableStatModel row)
		{
			return row.Победы * 3 + row.Ничьи;
		}

		private GameResult ComputeGameResult(List<GameEvents> gameEvents, TableStatModel row)
		{
			int goals = 0,
				fails = 0;

			foreach (var gameEvent in gameEvents)
			{
				if (gameEvent.EventID == 1 && _teamRepository.GetByPlayer(gameEvent.PlayerID).Name == row.Команда)
					goals++;
				if (gameEvent.EventID == 3 && _teamRepository.GetByPlayer(gameEvent.PlayerID).Name == row.Команда)
					fails++;
			}
			return goals > fails ? GameResult.Win : goals == fails ? GameResult.Draw : GameResult.Lose;
		}

		private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
		{
			dgTableStat.ItemsSource = GetTableStats().ToList();
		}
	}
}
