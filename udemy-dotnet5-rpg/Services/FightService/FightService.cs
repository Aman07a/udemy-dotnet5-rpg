﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using udemy_dotnet5_rpg.Data;
using udemy_dotnet5_rpg.DTOS.Fight;
using udemy_dotnet5_rpg.Models;

namespace udemy_dotnet5_rpg.Services.FightService
{
	public class FightService : IFightService
	{
		private readonly DataContext _context;

		public FightService(DataContext context)
		{
			_context = context;
		}

		public async Task<ServiceResponse<AttackResultDTO>> SkillAttack(SkillAttackDTO request)
		{
			var response = new ServiceResponse<AttackResultDTO>();

			try
			{
				var attacker = await _context.Characters
					.Include(c => c.Skills)
					.FirstOrDefaultAsync(c => c.Id == request.AttackerId);

				var opponent = await _context.Characters
					.FirstOrDefaultAsync(c => c.Id == request.OpponentId);

				var skill = attacker.Skills.FirstOrDefault(s => s.Id == request.SkillId);

				if (skill == null)
				{
					response.Success = false;
					response.Message = $"{attacker.Name} doesn't know this skill.";
					return response;
				}

				int damage = skill.Damage + (new Random().Next(attacker.Intelligence));
				damage -= new Random().Next(opponent.Defense);

				if (damage > 0)
					opponent.HitPoints -= damage;

				if (opponent.HitPoints <= 0)
					response.Message = $"{opponent.Name} has been defeated!";

				await _context.SaveChangesAsync();

				response.Data = new AttackResultDTO
				{
					Attacker = attacker.Name,
					AttackerHP = attacker.HitPoints,
					Opponent = opponent.Name,
					OpponentHP = opponent.HitPoints,
					Damage = damage
				};
			}
			catch (Exception ex)
			{
				response.Success = false;
				response.Message = ex.Message;
			}

			return response;
		}

		public async Task<ServiceResponse<AttackResultDTO>> WeaponAttack(WeaponAttackDTO request)
		{
			var response = new ServiceResponse<AttackResultDTO>();

			try
			{
				var attacker = await _context.Characters
					.Include(c => c.Weapon)
					.FirstOrDefaultAsync(c => c.Id == request.AttackerId);

				var opponent = await _context.Characters
					.FirstOrDefaultAsync(c => c.Id == request.OpponentId);

				int damage = attacker.Weapon.Damage + (new Random().Next(attacker.Strength));
				damage -= new Random().Next(opponent.Defense);

				if (damage > 0)
					opponent.HitPoints -= damage;

				if (opponent.HitPoints <= 0)
					response.Message = $"{opponent.Name} has been defeated!";

				await _context.SaveChangesAsync();

				response.Data = new AttackResultDTO
				{
					Attacker = attacker.Name,
					AttackerHP = attacker.HitPoints,
					Opponent = opponent.Name,
					OpponentHP = opponent.HitPoints,
					Damage = damage
				};
			}
			catch (Exception ex)
			{
				response.Success = false;
				response.Message = ex.Message;
			}

			return response;
		}
	}
}
