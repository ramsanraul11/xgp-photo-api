var builder = WebApplication.CreateBuilder(args);

// =======================================================
// ?? REGISTRO DE SERVICIOS
// =======================================================
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddAuthInfrastructure(builder.Configuration);
builder.Services.AddSwaggerDocumentation();
builder.Services.AddCors();
builder.Services.AddControllers();

// =======================================================
// ?? AUTENTICACIÓN (PIPELINE)
// =======================================================
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        var jwt = builder.Configuration.GetSection("Jwt").Get<JwtOptions>()!;
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwt.Issuer,
            ValidAudience = jwt.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key)),

            // ?? Esto es CLAVE para roles correctos
            RoleClaimType = ClaimTypes.Role,
            NameClaimType = ClaimTypes.Name
        };
    });

var app = builder.Build();

// =======================================================
// ?? PIPELINE HTTP
// =======================================================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// =======================================================
// ??? MIGRACIONES + SEEDING
// =======================================================
await app.Services.SeedDatabaseAsync();

// =======================================================
// ?? EJECUCIÓN
// =======================================================
app.Run();
