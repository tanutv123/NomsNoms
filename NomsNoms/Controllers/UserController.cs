﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NomsNoms.Data;
using NomsNoms.DTOs;
using NomsNoms.Entities;
using NomsNoms.Extensions;
using NomsNoms.Interfaces;

namespace NomsNoms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserController(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpPut("edit-profile")]
        /*[Authorize]*/
        public async Task<IActionResult> EditUserProfile([FromBody] UserProfileDTO profileDTO)
        {
            try
            {
                var user = _mapper.Map<AppUser>(profileDTO);
                await _userRepository.UpdateUserDetail(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
        [HttpPost("follow/{email}")]
        [Authorize]
        public async Task<IActionResult> UserFollow(string email)
        {
            if (email == User.GetEmail()) return BadRequest("You cannot follow yourself");
            await _userRepository.Follow(email, User.GetUserId());
            return Ok(new { message = "User followed successfully." });
        }
        [HttpGet("top10")]
        public async Task<IActionResult> GetTop10Cook()
        {
            return Ok(await _userRepository.GetTopCookByFollower());
        }
        [HttpGet("followers/{id}")]
        public async Task<IActionResult> GetFollowers(int id)
        {
            return Ok(await _userRepository.GetFollowerByCookId(id));
        }
        [HttpPost("subsctiption/buy/{subcriptionTypeId}")]
        [Authorize]
        public async Task<IActionResult> BuySubscription(string cookEmail, int subcriptionTypeId)
        {
            if (cookEmail == User.GetEmail()) return BadRequest("You cannot buy your own subscription");
            await _userRepository.BuySubscription(cookEmail ,User.GetEmail(), subcriptionTypeId);
            return Ok(new { message = "Buying Subscription successfully." });
        }
        [HttpGet("subsctiption/{cookEmail}")]
        [Authorize]
        public async Task<IActionResult> HasSubed(string cookEmail)
        {
            return Ok(await _userRepository.HasUserHasAlreadySub(cookEmail, User.GetEmail()));
        }
        [HttpGet("view/{recipeId}")]
        [Authorize]
        public async Task<IActionResult> ViewRecipe(int recipeId)
        {
            await _userRepository.RecipeView(recipeId);
            return Ok();
        }

    }
}
